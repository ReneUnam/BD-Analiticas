using Category;
namespace Operations.Category
{
    public class CategoryOperation
    {
        public void Excute()
        {
            //EXTRACT
            List<CategoryEntity> categoryEntities = new CategoryEntity().Get<CategoryEntity>();
            //TRANSFORM
            List<CategoryDIM> categoryDIMs = categoryEntities.Select(category => new CategoryDIM
            {
                Id_Categoria = category.Id_Categoria,
                Nombre = category.Nombre,
            }).ToList();
            //LOAD
            foreach (var categoryDim in categoryDIMs)
            {
                categoryDim.Save();
            }
        }
    }
}