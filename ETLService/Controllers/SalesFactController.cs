using Microsoft.AspNetCore.Mvc;
using Operations.Sales;
using System.Threading.Tasks;

namespace ETLService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesFactController : ControllerBase
    {
        private readonly SalesFactDashboardQuery _salesFactDashboardQuery;

        public SalesFactController(SalesFactDashboardQuery salesFactQuery)
        {
            _salesFactDashboardQuery = salesFactQuery;
        }

        [HttpGet("GetAggregatedSales")]
        public async Task<IActionResult> GetAggregatedSales()
        {
            var results = await _salesFactDashboardQuery.GetAggregatedSalesAsync();
            
            return Ok(results);
        }
    }
}