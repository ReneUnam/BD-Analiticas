using Operations.Category;
using Operations.Users;
using Operations.Producto;
using Operations.Units;
using Operations.Laboratory;
using iText.Kernel.Colors;
using Operations.Customer;
using Operations.Time;
using Operations.Sales;

namespace Operations;

public class StartServices
{
    public async Task<bool> StartServicesApp()
    {
        try
        {

            // await Task.Run(() => new UnitsOperation().Execute());
            // await Task.Run(() => new LaboratoryOperation().Execute());
            // await Task.Run(() => new CategoryOperation().Execute());
            // await Task.Run(() => new UsersOperation().Execute());
            // await Task.Run(() => new ProductoOperation().Execute());
            // await Task.Run(() => new CustomerOperation().Execute());
            // await Task.Run(() => new TimeOperation().Execute());
            await Task.Run(() => new SalesFactOperation().Execute());
            return true;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error al iniciar los servicios: {ex.Message}");
            throw;
        }
    }
}

