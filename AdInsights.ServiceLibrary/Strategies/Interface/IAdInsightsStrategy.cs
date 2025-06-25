using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdInsights.ServiceLibrary.Strategies.Interface;

public interface IAdInsightsStrategy
{
    Task<Int64> GetClicksByCampaignId(string campaignID, string tenantId);
}
