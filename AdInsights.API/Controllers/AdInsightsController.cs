using AdInsights.ServiceLibrary.Interface;
using AdInsights.ServiceLibrary.Interface;
using AdInsights.ServiceLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdInsights.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdInsightsController : BaseController
    {
        
        private readonly IAdInsightsService _adMetricsService;

        public AdInsightsController(IAdInsightsService adMetricsService, ITenantContextService tenantContextService)
            : base(tenantContextService)
        {
            _adMetricsService = adMetricsService;
        }

        [HttpGet("{campaignID}/clicks")]
        public async Task<IActionResult> GetAdClicks(string campaignID)
        {
            var clicks = await _adMetricsService.GetAdClicksByCampaignId(campaignID, TenantId);
            return Ok(clicks);
        }
    }
}
