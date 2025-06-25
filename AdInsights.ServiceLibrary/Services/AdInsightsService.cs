using AdInsights.DA.Repository.Interface;
using AdInsights.ServiceLibrary.Interface;
using AdInsights.ServiceLibrary.Strategies;
using AdInsights.ServiceLibrary.Strategies.Interface;


namespace AdInsights.ServiceLibrary.Services;

public class AdInsightsService : IAdInsightsService
{
    private readonly AdinsightsStrategyFactory _strategyFactory;
    private readonly IRedisAdInsightsRepository _redisRepository;


    public AdInsightsService(AdinsightsStrategyFactory strategyFactory, IRedisAdInsightsRepository redisRepository)
    {
        _strategyFactory = strategyFactory;
        _redisRepository = redisRepository;
    }
  

   

    public async Task<long> GetAdClicksByCampaignId(string campaignID, string tenantId)
    {
        var strategy = _strategyFactory.GetStrategy(true); // Fetch from Redis first
        var clicks = await strategy.GetClicksByCampaignId(campaignID, tenantId);

        if (clicks == 0)
        {
            strategy = _strategyFactory.GetStrategy(false); // Fallback to BigQuery
            clicks = await strategy.GetClicksByCampaignId(campaignID, tenantId);
        }

        return clicks;
    }

    public Task GetAdClicksByCampaignId(string campaignID, object tenantId)
    {
        throw new NotImplementedException();
    }
}
