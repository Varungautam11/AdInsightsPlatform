using AdInsights.DA.Repository.Interface;
using StackExchange.Redis;

namespace AdInsights.DA.Repository;

public class RedisAdInsightsRepository : IRedisAdInsightsRepository
{

    private readonly IDatabase _redis;

    public RedisAdInsightsRepository(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }


    public async Task<long> GetRealTimeClicksByCampaignId(string campaignID, string tenantId)
    {
        var clicks = await _redis.StringGetAsync($"clicks:{tenantId}_{campaignID}");
        return clicks.HasValue ? Convert.ToInt64(clicks) : 0;
    }

    public async Task<long> GetRealTimeClicksByCampaignIdAndAdId(string campaignID, string AdId, string tenantId)
    {
        var clicks = await _redis.StringGetAsync($"clicks:{tenantId}_{campaignID}_{AdId}");
        return clicks.HasValue ? Convert.ToInt64(clicks) : 0; ;
    }
}
