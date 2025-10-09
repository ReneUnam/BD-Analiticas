using Client;
using System.Linq;

namespace Operations.Client
{
    public class ClientOperation
    {
        public void Excute()
        {
            // EXTRACT
            List<Cliente> clients = new Cliente().Get<Cliente>();
            // TRANSFORM
            List<DimCliente> dims = clients.Select(c => new DimCliente
            {
                IdCliente = c.IdCliente,
                Nombre = c.Nombre,
                Apellido = c.Apellido
            }).ToList();
            // LOAD
            foreach (var d in dims)
            {
                Console.WriteLine($"DimCliente: {d.IdCliente} - {d.Nombre} {d.Apellido}");
                d.Save();
            }
        }
    }
}
