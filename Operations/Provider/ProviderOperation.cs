using Provider;
using System.Linq;

namespace Operations.Provider
{
    public class ProviderOperation
    {
        public void Excute()
        {
            var provs = new Proveedor().Get<Proveedor>();
            var dims = provs.Select(p => new DimProveedor
            {
                IdProveedor = p.IdProveedor,
                Nombre = p.Nombre,
                Telefono = p.Telefono
            }).ToList();

            foreach (var d in dims)
            {
                Console.WriteLine($"DimProveedor: {d.IdProveedor} - {d.Nombre}");
                d.Save();
            }
        }
    }
}
