using Operations.Category;
using Operations.Usuarios;

namespace Operations;

public class StartServices
{
    public async Task<bool> StartServicesApp()
    {
        try
        {
            new CategoryOperation().Excute();
            new UsuarioOperation().Excute();
            return true;

         
        }
        catch (System.Exception ex)
        {
            throw;
        }
    }
}

