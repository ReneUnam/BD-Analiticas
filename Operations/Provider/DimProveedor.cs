using APPCORE;
using BusinessLogic.Connection;
namespace Provider
{
	class DimProveedor : EntityClass
	{
		public DimProveedor()
		{
			this.MDataMapper = new BDConnection().DBDestino;
		}

		[PrimaryKey(Identity = false)]
		public int? IdProveedor { get; set; }
		public string? Nombre { get; set; }
		public string? Telefono { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
