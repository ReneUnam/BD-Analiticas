using System;
using System.Collections.Generic;
using System.Linq;
using Producto;
using Category;
using Laboratory;
using Units;

namespace Operations.Producto
{
    public class ProductoOperation
    {
        public void Execute()
        {
            Console.WriteLine("=== INICIANDO ETL DE PRODUCTOS (DETALLE) ===");

            // ===== EXTRACT (OLTP)
            List<Cat_Producto> productos = new Cat_Producto().Get<Cat_Producto>();
            List<Cat_DetalleProducto> detalles = new Cat_DetalleProducto().Get<Cat_DetalleProducto>();
            List<Tbl_ProductoAlmacenado> almacenados = new Tbl_ProductoAlmacenado().Get<Tbl_ProductoAlmacenado>();

            // ===== EXTRACT (DIM ya cargadas)
            List<CategoriasDIM> categoriaDims = new CategoriasDIM().Get<CategoriasDIM>();
            List<LaboratorioDIM> laboratorioDims = new LaboratorioDIM().Get<LaboratorioDIM>();
            List<UnidadesDIM> unidadDims = new UnidadesDIM().Get<UnidadesDIM>();

            // ===== TRANSFORM
            var productoDIMs = (
                from d in detalles
                join p in productos on d.Detalle_IdProducto equals p.IdProducto
                join a in almacenados on d.Detalle_Id equals a.Almc_Detalle_Id into almcJoin
                from a in almcJoin.DefaultIfEmpty()

                // joins con las dimensiones ya cargadas
                join c in categoriaDims on p.IdCategoria equals c.IdCategoria into catJoin
                from c in catJoin.DefaultIfEmpty()

                join l in laboratorioDims on p.IdLaboratorio equals l.IdLaboratorio into labJoin
                from l in labJoin.DefaultIfEmpty()

                join u in unidadDims on d.Detalle_IdUnidadMedida equals u.IdUnidad into uniJoin
                from u in uniJoin.DefaultIfEmpty()

                select new ProductoDIM
                {
                    IdProductoOLTP = d.Detalle_Id, // cada detalle es un producto único
                    NombreProducto = $"{p.Nombre} - {d.Detalle_Descripcion}".Trim(),
                    NombreCategoria = c?.Nombre ?? "Sin categoría",
                    NombreLaboratorio = l?.Nombre ?? "Sin laboratorio",
                    NombreUnidad = u?.Nombre ?? "Sin unidad",
                    FechaVencimiento = d.Detalle_FechaVencimiento,
                    PrecioVenta = a?.Almc_PrecioVenta ?? 0m,
                    PrecioCompra = a?.Almc_PrecioCompra ?? 0m,
                    FechaCargaDW = DateTime.Now
                }
            ).ToList();

            // ===== LOAD
            Console.WriteLine($"[INFO] Total productos detallados a cargar: {productoDIMs.Count}");
            foreach (var prod in productoDIMs)
            {
                try
                {
                    prod.Save();
                    Console.WriteLine($"Cargado: {prod.IdProductoOLTP} - {prod.NombreProducto}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Producto {prod.IdProductoOLTP}: {ex.Message}");
                }
            }

            Console.WriteLine("=== ETL DE PRODUCTOS (DETALLE) COMPLETADO ===");
        }
    }
}
