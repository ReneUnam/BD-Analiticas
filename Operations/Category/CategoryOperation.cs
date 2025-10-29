using APPCORE;
using Category;
namespace Operations.Category
{
    public class CategoryOperation
    {
        public void Execute()
        {
            //EXTRACT
            List<Categorias> categoryEntities = new Categorias().Where<Categorias>(
                FilterData.Greater("Updated_At", DateOLAPOperation.GetLastUpdatedate())
            );
            // categoryEntities = new Categorias().Get<Categorias>();
            //TRANSFORM
            List<CategoriasDIM> categoryDIMs = categoryEntities.Select(category => new CategoriasDIM
            {
                IdCategoria = category.IdCategoria,
                Nombre = category.Nombre,
                Descripcion = category.Descripcion,
            }).ToList();
            //LOAD
            foreach (var categoryDim in categoryDIMs)
            {
                if (categoryDim.Exists())
                    continue;
                else
                    categoryDim.Save();
                    DateOLAPOperation.UpdateLastUpdateDate(DateTime.Now);
                    
                Console.WriteLine($"{categoryDim.IdCategoria} - {categoryDim.Nombre}");
            }

        }
    }
}