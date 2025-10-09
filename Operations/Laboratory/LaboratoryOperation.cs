using Laboratory;
using System.Linq;

namespace Operations.Laboratory
{
    public class LaboratoryOperation
    {
        public void Excute()
        {
            var labs = new Laboratorio().Get<Laboratorio>();
            var dims = labs.Select(l => new DimLaboratorio
            {
                IdLaboratorio = l.IdLaboratorio,
                Nombre = l.Nombre,
                Descripcion = l.Descripcion,
                CreatedAt = DateTime.Now
            }).ToList();

            foreach (var d in dims)
            {
                Console.WriteLine($"DimLaboratorio: {d.IdLaboratorio} - {d.Nombre}");
                d.Save();
            }
        }
    }
}
