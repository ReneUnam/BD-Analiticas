using System;
using System.Linq;
using Provider;
using User;
using APPCORE;
using Time;
using Operations.Connections.Purchases;

namespace Operations.ETL
{
    public class FactComprasOperation
    {
        public void Excute()
        {
            // Use typed source entity for reading from origin
            var compras = new Operations.Purchases.ComprasSource().Get<Operations.Purchases.ComprasSource>();

            foreach (var c in compras)
            {
                try
                {
                    if (c.Fecha == null) continue;
                    var date = ((DateTime)c.Fecha).Date;

                    // Try to get existing DimTiempo by Fecha
                    var tiempo = new DimTiempo().SimpleFind<DimTiempo>(FilterData.Equal("Fecha", date));
                    if (tiempo == null)
                    {
                        var tnew = new DimTiempo { Fecha = date, Año = date.Year, Mes = date.Month, Dia = date.Day };
                        tnew.Save();
                        tiempo = tnew;
                    }

                    var fact = new FactCompras
                    {
                        IdCompra = c.IdCompra,
                        IdTiempo = tiempo.IdTiempo,
                        IdProveedor = c.IdProveedor,
                        IdUsuario = c.IdUsuario,
                        IdProducto = c.IdProducto,
                        Cantidad = c.Cantidad,
                        PrecioUnitario = c.PrecioUnitario,
                        Total = c.Total
                    };

                    fact.Save();
                    Console.WriteLine($"Inserted FactCompra {c.IdCompra}");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"FactCompras error: {ex.Message}");
                }
            }
        }
    }
}
