using APPCORE;
using BusinessLogic.Connection;

namespace Operations.Sales
{
    public class FactVentas : EntityClass
    {
        public FactVentas()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }

        public int Fecha_Key { get; set; }
        public int Producto_Key { get; set; }
        public int Cliente_Key { get; set; }
        public int Vendedor_Key { get; set; }
        public int IdDetalleVenta_OLTP { get; set; }

        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
    }
}
