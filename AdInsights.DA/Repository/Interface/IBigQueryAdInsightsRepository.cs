using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdInsights.DA.Repository.Interface;

public interface IBigQueryAdInsightsRepository
{
    public Task<Int64> GetRealTimeClicksByCampaignId(string campaignId, string tenantId);
    Task<Int64> GetClicksByCampaignIdAndLookbackPeriod(string campaignID, string tenantId, TimeSpan lookbackPeriod);
}

