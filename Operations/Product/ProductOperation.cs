using Product;
using System.Linq;

namespace Operations.Product
{
    public class ProductOperation
    {
        public void Excute()
        {
            var products = new Producto().Get<Producto>();
            var dims = products.Select(p => new DimProducto
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                IdCategoria = p.IdCategoria,
                IdLaboratorio = p.IdLaboratorio,
                CreatedAt = p.CreatedAt
            }).ToList();

            foreach (var d in dims)
            {
                Console.WriteLine($"DimProducto: {d.IdProducto} - {d.Nombre}");
                d.Save();
            }
        }
    }
}
