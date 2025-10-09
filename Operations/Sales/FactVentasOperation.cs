using System;
using System.Linq;
using Product;
using Client;
using User;
using APPCORE;
using Time;
using Operations.Sales;

namespace Operations.ETL
{
    public class FactVentasOperation
    {
        public void Excute()
        {
            // Use typed source entity for reading from origin
            var ventas = new VentasSource().Get<VentasSource>();

            foreach (var v in ventas)
            {
                try
                {
                    if (v.Fecha == null) continue;
                    var date = ((DateTime)v.Fecha).Date;
                    var tiempo = new DimTiempo().SimpleFind<DimTiempo>(FilterData.Equal("Fecha", date));
                    if (tiempo == null)
                    {
                        var tnew = new DimTiempo { Fecha = date, Año = date.Year, Mes = date.Month, Dia = date.Day };
                        tnew.Save();
                        tiempo = tnew;
                    }

                    var fact = new FactVentas
                    {
                        IdVenta = v.IdVenta,
                        IdTiempo = tiempo.IdTiempo,
                        IdCliente = v.IdCliente,
                        IdUsuario = v.IdUsuario,
                        IdProducto = v.IdProducto,
                        Cantidad = v.Cantidad,
                        PrecioUnitario = v.PrecioUnitario,
                        Descuento = v.Descuento,
                        Total = v.Total
                    };

                    fact.Save();
                    Console.WriteLine($"Inserted FactVenta {v.IdVenta}");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"FactVentas error: {ex.Message}");
                }
            }
        }
    }
}
