using APPCORE;
using Supplier;
namespace Operations.Supplier
{
    public class SupplierOperation
    {
        public void Execute()
        {
            DateTime startTime = DateTime.Now;
            //EXTRACT
            List<Proveedores> supplierEntities = new Proveedores().Where<Proveedores>(
                FilterData.Greater("Updated_At", HistoricDateOLAPOperation.GetLastUpdatedate())
            );
            //TRANSFORM
            List<ProveedoresDIM> supplierDIMs = supplierEntities.Select(supplier => new ProveedoresDIM
            {
                IdProveedor = supplier.IdProveedor,
                Nombre = supplier.Nombre,
            }).ToList();

            //LOAD
            int registeredRows = 0;
            foreach (var supplierDim in supplierDIMs)
            {
                var existingSupplier = new ProveedoresDIM().Find<ProveedoresDIM>(
                    FilterData.Equal("IdProveedor", supplierDim.IdProveedor)
                );
                if (existingSupplier != null)
                {
                    supplierDim.Update();
                }
                else
                {
                    supplierDim.Save();
                    registeredRows++;
                }

                Console.WriteLine($"{supplierDim.IdProveedor} - {supplierDim.Nombre}");
            }
            DateTime endTime = DateTime.Now;
            HistoricDateOLAPOperation.UpdateLastUpdateDate(startTime, endTime, registeredRows);
        }
    }
}