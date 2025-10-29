using APPCORE;
using Category;
namespace Operations.Category
{
    public class CategoryOperation
    {
        public void Execute()
        {
            DateTime startTime = DateTime.Now;
            //EXTRACT
            List<Categorias> categoryEntities = new Categorias().Where<Categorias>(
                FilterData.Greater("Updated_At", HistoricDateOLAPOperation.GetLastUpdatedate())
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
            int registeredRows = 0;
            foreach (var categoryDim in categoryDIMs)
            {
                if (categoryDim.Exists())
                    continue;
                else
                {
                    categoryDim.Save();
                    registeredRows++;
                }

                Console.WriteLine($"{categoryDim.IdCategoria} - {categoryDim.Nombre}");
            }
            DateTime endTime = DateTime.Now;
            HistoricDateOLAPOperation.UpdateLastUpdateDate(startTime, endTime, registeredRows);

        }
    }
}