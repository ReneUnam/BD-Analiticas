using User;
using System.Linq;

namespace Operations.User
{
    public class UserOperation
    {
        public void Excute()
        {
            var users = new Usuario().Get<Usuario>();
            var dims = users.Select(u => new DimUsuario
            {
                IdUsuario = u.IdUsuario,
                NombreUsuario = u.NombreUsuario,
                Nombres = u.Nombres,
                Apellidos = u.Apellidos,
                IdRol = u.IdRol
            }).ToList();

            foreach (var d in dims)
            {
                Console.WriteLine($"DimUsuario: {d.IdUsuario} - {d.NombreUsuario}");
                d.Save();
            }
        }
    }
}
