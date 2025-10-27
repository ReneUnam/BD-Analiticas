using Users;
namespace Operations.Users
{
    public class UsersOperation
    {
        public void Execute()
        {
            //EXTRACT
            List<Usuarios> usuarioEntities = new Usuarios().Get<Usuarios>();
            //TRANSFORM
            List<UsuarioDIM> usuarioDIMs = usuarioEntities.Select(usuarios => new UsuarioDIM
            {
                IdUsuario = usuarios.IdUsuario,
                Nombres = usuarios.Nombres,
                Apellidos = usuarios.Apellidos,
                NombreUsuario = usuarios.NombreUsuario,
            }).ToList();
            //LOAD
            foreach (var usuarioDim in usuarioDIMs)
            {
                Console.WriteLine($"{usuarioDim.IdUsuario} - {usuarioDim.Nombres}- {usuarioDim.Apellidos}- {usuarioDim.NombreUsuario}");
                usuarioDim.Save();
            }
        }
    }
}