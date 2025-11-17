using Microsoft.AspNetCore.Mvc;
using Operations.Purchases;

namespace ETLService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PurchasesFactController : ControllerBase
    {
        private readonly PurchasesFactDashboardQuery _purchasesFactDashboardQuery;

        public PurchasesFactController(PurchasesFactDashboardQuery purchasesFactQuery)
        {
            _purchasesFactDashboardQuery = purchasesFactQuery;
        }

        [HttpGet("GetAggregatedPurchases")]
        public async Task<IActionResult> GetAggregatedPurchases()
        {
            var results = await _purchasesFactDashboardQuery.GetAggregatedPurchasesAsync();

            return Ok(results);
        }
    }
}