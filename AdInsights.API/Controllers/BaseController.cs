using AdInsights.ServiceLibrary.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AdInsights.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ITenantContextService _tenantContextService;

        protected BaseController(ITenantContextService tenantContextService)
        {
            _tenantContextService = tenantContextService;
        }

        protected string TenantId => _tenantContextService.TenantId;
    }

}
