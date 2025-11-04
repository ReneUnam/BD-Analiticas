namespace Operations.Time 
{ 
    public class TimeOperation 
    { 
        public void Execute() 
        {  
            var beginTime = new DateTime(2024,1,1);
            var endTime = DateTime.Now; 
            Console.WriteLine($"Inicio: {beginTime}, Fin: {endTime}");
             
             //EXTRACT - TRANSFORM 
            List<TimeDIM> entitys = TimeGenerator.Generar(beginTime, endTime.AddDays(1));
            Console.WriteLine($"Generando {entitys.Count} fechas desde {beginTime.Date} hasta {endTime.Date}");

            //LOAD 
            foreach (var entity in entitys) 
            { 
                if (new TimeDIM { FechaKey = entity.FechaKey } 
                    .SimpleFind<TimeDIM>() != null) 
                { 
                    continue; 
                } 
                entity.Save(); 
            } 
             
        } 
 
    } 
} 