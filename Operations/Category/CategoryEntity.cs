using APPCORE;
using BusinessLogic.Connection;
namespace Category
{
    class CategoryEntity : EntityClass
    {
        public CategoryEntity()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }
        [PrimaryKey(Identity = true)]
        public int? Id_Category { get; set; }
        public string? Name { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}