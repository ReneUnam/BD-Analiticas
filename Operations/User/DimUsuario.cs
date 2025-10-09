using APPCORE;
using BusinessLogic.Connection;
namespace User
{
	class DimUsuario : EntityClass
	{
		public DimUsuario()
		{
			this.MDataMapper = new BDConnection().DBDestino;
		}

		[PrimaryKey(Identity = false)]
		public int? IdUsuario { get; set; }
		public string? NombreUsuario { get; set; }
		public string? Nombres { get; set; }
		public string? Apellidos { get; set; }
		public int? IdRol { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
