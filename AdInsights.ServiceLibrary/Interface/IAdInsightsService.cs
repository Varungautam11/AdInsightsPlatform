

namespace AdInsights.ServiceLibrary.Interface;

public interface IAdInsightsService
{
    Task<Int64> GetAdClicksByCampaignId(string campaignID, string tenantId);
}
