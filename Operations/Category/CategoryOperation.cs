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
                Id_Category = category.Id_Category,
                Name = category.Name,
            }).ToList();
            //LOAD
            foreach (var categoryDim in categoryDIMs)
            {
                categoryDim.Save();
            }
        }
    }
}