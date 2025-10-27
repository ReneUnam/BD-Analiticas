using Units;
namespace Operations.Units
{
    public class UnitsOperation
    {
        public void Execute()
        {
            // EXTRACT
            List<Unidades> unidades = new Unidades().Get<Unidades>();
            // TRANSFORM
            List<UnidadesDIM> unidadesDIMs = unidades.Select(u => new UnidadesDIM
            {
                IdUnidad = u.IdUnidad,
                Nombre = u.Nombre,
                Abreviatura = u.Abreviatura
            }).ToList();

            // LOAD
            foreach (var unidadDim in unidadesDIMs)
            {
                Console.WriteLine($"{unidadDim.IdUnidad} - {unidadDim.Nombre} ({unidadDim.Abreviatura})");
                unidadDim.Save();
            }
        }
    }
}
