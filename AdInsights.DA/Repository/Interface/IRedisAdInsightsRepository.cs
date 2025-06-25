using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdInsights.DA.Repository.Interface
{
    public interface IRedisAdInsightsRepository
    {
        public Task<Int64> GetRealTimeClicksByCampaignId(string campaignID, string tenantId);
        Task<Int64> GetRealTimeClicksByCampaignIdAndAdId(string campaignID, string AdId, string tenantId);
    }
}
