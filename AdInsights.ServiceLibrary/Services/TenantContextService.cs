using AdInsights.ServiceLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdInsights.ServiceLibrary.Services;

public class TenantContextService : ITenantContextService
{
    public string TenantId { get; private set; } = string.Empty;

    public void SetTenantId(string tenantId)
    {
        TenantId = tenantId;
    }
}
