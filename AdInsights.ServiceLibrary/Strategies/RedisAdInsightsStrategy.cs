using AdInsights.ServiceLibrary.Strategies.Interface;
using AdInsights.DA.Repository.Interface;

namespace AdInsights.ServiceLibrary.Strategies;

public class RedisAdInsightsStrategy : IAdInsightsStrategy
{
    private readonly ICacheStrategy _cacheStrategy;

    public RedisAdInsightsStrategy(ICacheStrategy cacheStrategy)
    {
        _cacheStrategy = cacheStrategy;
    }
    public async Task<long> GetClicksByCampaignId(string campaignID, string tenantId)
    {
        return await _cacheStrategy.GetClicksByCampaignId(campaignID, tenantId);
    }

    public async Task<long> GetClicksByCampaignIdAndAdId(string campaignID, string AdId, string tenantId)
    {
        return await _cacheStrategy.GetClicksByCampaignIdAndAdId(campaignID, AdId, tenantId);
    }

}
