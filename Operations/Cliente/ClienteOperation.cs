using APPCORE;
using Customer;
using MySqlX.XDevAPI;

namespace Operations.Customer
{
    public class CustomerOperation
    {
        public void Execute()
        {
            DateTime startTime = DateTime.Now;

            // EXTRACT: todos los clientes (no hay Updated_At en Cliente)
            List<Clientes> clientes = new Clientes().Where<Clientes>(
                FilterData.Greater("Updated_At", HistoricDateOLAPOperation.GetLastUpdatedate())
            );

            // TRANSFORM: concatenar Nombre + Apellido en el campo Nombre del DIM
            List<ClienteDIM> clienteDIMs = clientes.Select(c => new ClienteDIM
            {
                IdCliente = c.IdCliente,
                Nombre = $"{c.Nombre ?? ""} {c.Apellido ?? ""}".Trim()
            }).ToList();

            // LOAD: insertar si no existe (igual que categoría)
            int registeredRows = 0;
            foreach (var dim in clienteDIMs)
            {
                if (dim.Exists())
                    continue;

                dim.Save();
                registeredRows++;

                Console.WriteLine($"{dim.IdCliente} - {dim.Nombre}");
            }

            DateTime endTime = DateTime.Now;
            HistoricDateOLAPOperation.UpdateLastUpdateDate(startTime, endTime, registeredRows);
        }
    }
}