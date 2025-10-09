using APPCORE;
using BusinessLogic.Connection;
namespace Provider
{
	class Proveedor : EntityClass
	{
		public Proveedor()
		{
			this.MDataMapper = new BDConnection().DBOrigen;
		}

		[PrimaryKey(Identity = true)]
		public int? IdProveedor { get; set; }
		public string? Nombre { get; set; }
		public string? Telefono { get; set; }
		public bool? Estado { get; set; }
	}
}
