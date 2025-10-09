using Usuarios;
namespace Operations.Usuarios
{
    public class UsuarioOperation
    {
        public void Excute()
        {
            //EXTRACT
            List<Usuario> usuarioEntities = new Usuario().Get<Usuario>();
            //TRANSFORM
            List<UsuarioDIM> usuarioDIMs = usuarioEntities.Select(usuarios => new UsuarioDIM
            {
                IdUsuario = usuarios.IdUsuario,
                Nombres = usuarios.Nombres,
                Apellidos = usuarios.Apellidos,
                NombreUsuario = usuarios.NombreUsuario,
                UsuarioSalt = usuarios.UsuarioSalt,
                Contraseña = usuarios.Contraseña,
                IdRol = usuarios.IdRol,
                Estado = usuarios.Estado
            }).ToList();
            //LOAD
            foreach (var usuarioDim in usuarioDIMs)
            {
                Console.WriteLine($"{usuarioDim.IdUsuario} - {usuarioDim.Nombres}- {usuarioDim.Apellidos}- {usuarioDim.NombreUsuario}- {usuarioDim.UsuarioSalt}- {usuarioDim.Contraseña}");
                usuarioDim.Save();
            }
        }
    }
}