using Operations.Category;
using Operations.Client;
using Operations.Provider;
using Operations.User;
using Operations.Product;
using Operations.Laboratory;
using Operations.ETL;

namespace Operations;

public class StartServices
{
    public async Task<bool> StartServicesApp()
    {
        try
        {
            // Dimensions
            new CategoryOperation().Excute();
            new ClientOperation().Excute();
            new ProviderOperation().Excute();
            new UserOperation().Excute();
            new ProductOperation().Excute();

            // Facts
            new FactVentasOperation().Excute();
            new FactComprasOperation().Excute();

            return true;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"StartServices error: {ex.Message}");
            throw;
        }
    }
}