using AdInsights.DA.Repository.Interface;
using Google.Cloud.BigQuery.V2;

namespace AdInsights.DA.Repository;

internal class BigQueryAdInsightsRepository : IBigQueryAdInsightsRepository
{

    private readonly BigQueryClient _bigQueryClient;

    public BigQueryAdInsightsRepository(BigQueryClient bigQueryClient)
    {
        _bigQueryClient = bigQueryClient;
    }

    public async Task<long> GetClicksByCampaignIdAndLookbackPeriod(string campaignID, string tenantId, TimeSpan lookbackPeriod)
    {
        // Calculate the timestamp window
        DateTime startTime = DateTime.UtcNow.Subtract(lookbackPeriod);

        string query = $@"
            SELECT SUM(event_count) AS total_clicks
            FROM `AdServer.EventData.ad_events`
            WHERE campaignID = @campaignID 
            AND tenant_id = @tenantId
            AND eventType = 'click'
            AND timestamp >= @startTime";

        var parameters = new List<BigQueryParameter>
            {
                new BigQueryParameter("campaignID", BigQueryDbType.String, campaignID),
                new BigQueryParameter("tenantId", BigQueryDbType.String, tenantId),
                new BigQueryParameter("startTime", BigQueryDbType.Timestamp, startTime)
            };

        BigQueryResults results = await _bigQueryClient.ExecuteQueryAsync(query, parameters);

        Int64 totalClicks = 0;
        foreach (var row in results)
        {
            totalClicks = Convert.ToInt64(row["total_clicks"]);
        }
        return totalClicks;
    }

    public async Task<long> GetRealTimeClicksByCampaignId(string campaignID, string tenantId)
    {
        string query = $@"
            SELECT SUM(event_count) AS total_clicks
            FROM `AdServer.EventData.ad_events`
            WHERE campaignID = @campaignID AND tenant_id = @tenantId";

        var parameters = new List<BigQueryParameter>
            {
                new BigQueryParameter("campaignID", BigQueryDbType.String, campaignID),
                new BigQueryParameter("tenantId", BigQueryDbType.String, tenantId)
            };

        BigQueryResults results = await _bigQueryClient.ExecuteQueryAsync(query, parameters);

        Int64 totalClicks = 0;
        foreach (var row in results)
        {
            totalClicks = Convert.ToInt64(row["total_clicks"]);
        }
        return totalClicks;
    }
}
