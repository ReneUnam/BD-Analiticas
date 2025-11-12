using APPCORE;
using BusinessLogic.Connection;

namespace Operations.Purchases
{
    public class FactCompras : EntityClass
    {
        public FactCompras()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }

        public int Fecha_Key { get; set; }
        public int Producto_Key { get; set; }
        public int Proveedor_Key { get; set; }
        public int Usuario_Key { get; set; }
        public int IdDetalleCompra_OLTP { get; set; }

        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}