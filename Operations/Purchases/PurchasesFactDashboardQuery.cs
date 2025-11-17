using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using APPCORE.BDCore.Abstracts;
using BusinessLogic.Connection;

namespace Operations.Purchases
{
    public class PurchasesFactDashboardQuery
    {
        private readonly WDataMapper _dataMapper = new BDConnection().DBDestino;

        public async Task<List<PurchaseAggregateDTO>> GetAggregatedPurchasesAsync()
        {
            string query = GetAggregatedPurchaseQuery();
            var dt = this._dataMapper?.GDatos.TraerDatosSQL(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                return ConvertDataTableToDTO(dt);
            }
            return new List<PurchaseAggregateDTO>();
        }

        private string GetAggregatedPurchaseQuery()
        {
            return $@"
                SELECT 
                    T.Anio AS Year,                     
                    S.Nombre AS SupplierName,
                    P.NombreLaboratorio AS Laboratory,
                    
                    SUM(F.Cantidad) AS UnitsPurchased,        
                    SUM(F.Total) AS TotalCostPurchase 
                FROM 
                    FactCompras F
                INNER JOIN 
                    TimeDIM T ON F.Fecha_Key = T.FechaKey
                INNER JOIN 
                    ProductoDIM P ON F.Producto_Key = P.IdProductoOLTP
                INNER JOIN
                    ProveedoresDIM S ON F.Proveedor_Key = S.IdProveedor
                GROUP BY 
                    T.Anio, S.Nombre, P.NombreLaboratorio
                ORDER BY 
                    T.Anio, S.Nombre;
            ";
        }

        private List<PurchaseAggregateDTO> ConvertDataTableToDTO(DataTable dt)
        {
            var list = new List<PurchaseAggregateDTO>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new PurchaseAggregateDTO
                {
                    Year = Convert.ToInt32(row["Year"]),
                    SupplierName = row["SupplierName"].ToString(),
                    Laboratory = row["Laboratory"].ToString(),
                    TotalCostPurchase = Convert.ToDecimal(row["TotalCostPurchase"]),
                    UnitsPurchased = Convert.ToInt32(row["UnitsPurchased"])
                });
            }
            return list;
        }
    }
}