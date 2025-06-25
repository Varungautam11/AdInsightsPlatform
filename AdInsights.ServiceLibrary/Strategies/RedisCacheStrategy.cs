using AdInsights.ServiceLibrary.Strategies.Interface;
using AdInsights.DA.Repository.Interface;

namespace AdInsights.ServiceLibrary.Strategies;

public class RedisCacheStrategy : ICacheStrategy
{

    private readonly IRedisAdInsightsRepository _redisRepository;

    public RedisCacheStrategy(IRedisAdInsightsRepository redisRepository)
    {
        _redisRepository = redisRepository;
    }
    public async Task<long> GetClicksByCampaignId(string campaignID, string tenantId)
    {
        var clicks = await _redisRepository.GetRealTimeClicksByCampaignId(campaignID, tenantId);
        return clicks > 0 ? Convert.ToInt64(clicks) : 0;
    }

    public async Task<long> GetClicksByCampaignIdAndAdId(string campaignID, string AdId, string tenantId)
    {
        var clicks = await _redisRepository.GetRealTimeClicksByCampaignIdAndAdId(campaignID, AdId, tenantId);
        return clicks > 0 ? Convert.ToInt64(clicks) : 0;
    }
}
