using Operations.Category;

namespace Operations;

public class StartServices
{
    public async Task<bool> StartServicesApp()
    {
        try
        {
            new CategoryOperation().Excute();
            return true;
        }
        catch (System.Exception ex)
        {
            throw;
        }
    }
}