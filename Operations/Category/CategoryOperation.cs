using Category;
namespace Operations.Category
{
    public class CategoryOperation
    {
        public void Excute()
        {
            //EXTRACT
            List<Categorias> categoryEntities = new Categorias().Get<Categorias>();
            //TRANSFORM
            List<CategoriasDIM> categoryDIMs = categoryEntities.Select(category => new CategoriasDIM
            {
                IdCategoria = category.IdCategoria,
                Nombre = category.Nombre,
                Descripcion = category.Descripcion,
                Estado = category.Estado
            }).ToList();
            //LOAD
            foreach (var categoryDim in categoryDIMs)
            {
                Console.WriteLine($"{categoryDim.IdCategoria} - {categoryDim.Nombre}");
                categoryDim.Save();
            }
        }
    }
}