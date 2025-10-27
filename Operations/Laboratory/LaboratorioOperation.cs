using Laboratory;
namespace Operations.Laboratory
{
    public class LaboratoryOperation
    {
        public void Execute()
        {
            //EXTRACT
            List<Laboratorio> laboratorioEntities = new Laboratorio().Get<Laboratorio>();
            //TRANSFORM
            List<LaboratorioDIM> laboratorioDIMs = laboratorioEntities.Select(lab => new LaboratorioDIM
            {
                IdLaboratorio = lab.IdLaboratorio,
                Nombre = lab.Nombre,
            }).ToList();
            //LOAD
            foreach (var laboratorio in laboratorioDIMs)
            {
                Console.WriteLine($"{laboratorio.IdLaboratorio} - {laboratorio.Nombre}");
                laboratorio.Save();
            }
        }
    }
}