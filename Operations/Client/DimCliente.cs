using APPCORE;
using BusinessLogic.Connection;
namespace Client
{
	class DimCliente : EntityClass
	{
		public DimCliente()
		{
			this.MDataMapper = new BDConnection().DBDestino;
		}

		[PrimaryKey(Identity = false)]
		public int? IdCliente { get; set; }
		public string? Nombre { get; set; }
		public string? Apellido { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
