using Operations.Sales;
using System.Data;
using BusinessLogic.Connection;
using APPCORE.BDCore.Abstracts;

namespace Operations.Sales
{
    public class SalesFactDashboardQuery 
    {
        private readonly WDataMapper _dataMapper = new BDConnection().DBDestino; 
        
        public async Task<List<SalesDTO>> GetAggregatedSalesAsync()
        {
            string query = GetAggregatedQuery();
            
            var dt = this._dataMapper?.GDatos.TraerDatosSQL(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                return ConvertDataTableToDTO(dt); 
            }
            return new List<SalesDTO>();
        }

        private string GetAggregatedQuery()
        {
            return $@"
                SELECT 
                    T.Anio AS Year,                     
                    P.NombreCategoria AS Category,      
                    SUM(F.Total) AS TotalSale,          
                    SUM(F.Cantidad) AS UnitsSold        
                FROM 
                    FactVentas F
                INNER JOIN 
                    TimeDIM T ON F.Fecha_Key = T.FechaKey
                INNER JOIN 
                    ProductoDIM P ON F.Producto_Key = P.IdProductoOLTP
                GROUP BY 
                    T.Anio, P.NombreCategoria
                ORDER BY 
                    T.Anio, P.NombreCategoria;
            ";
        }

        private List<SalesDTO> ConvertDataTableToDTO(DataTable dt)
        {
            var list = new List<SalesDTO>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SalesDTO
                {
                    Year = Convert.ToInt32(row["Year"]),
                    Category = row["Category"].ToString(),
                    TotalSale = Convert.ToDecimal(row["TotalSale"]),
                    UnitsSold = Convert.ToInt32(row["UnitsSold"])
                });
            }
            return list;
        }
    }
}