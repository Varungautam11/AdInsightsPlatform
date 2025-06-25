using AdInsights.DA.Repository.Interface;
using AdInsights.ServiceLibrary.Strategies.Interface;

namespace AdInsights.ServiceLibrary.Strategies;

public class BigQueryAdInsightsStrategy : IAdInsightsStrategy
{
    private readonly IBigQueryAdInsightsRepository _bigQueryRepository;

    public BigQueryAdInsightsStrategy(IBigQueryAdInsightsRepository bigQueryRepository)
    {
        _bigQueryRepository = bigQueryRepository;
    }
    public async Task<long> GetClicksByCampaignId(string campaignID, string tenantId)
    {
        return await _bigQueryRepository.GetRealTimeClicksByCampaignId(campaignID, tenantId);
    }
}
