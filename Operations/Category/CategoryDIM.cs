using APPCORE;
using BusinessLogic.Connection;
namespace Category
{
    class CategoryDIM : EntityClass
    {
        public CategoryDIM()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }
        [PrimaryKey(Identity = false)]
        public int? Id_Category { get; set; }
        public string? Name { get; set; }
    }
}