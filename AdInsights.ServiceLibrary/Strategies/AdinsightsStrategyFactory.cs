using AdInsights.ServiceLibrary.Strategies.Interface;
namespace AdInsights.ServiceLibrary.Strategies;
public class AdinsightsStrategyFactory : IAdInsightsStrategyfactory
{

    private readonly RedisAdInsightsStrategy _redisStrategy;
    private readonly BigQueryAdInsightsStrategy _bigQueryStrategy;

    public AdinsightsStrategyFactory(RedisAdInsightsStrategy redisStrategy, BigQueryAdInsightsStrategy bigQueryStrategy)
    {
        _redisStrategy = redisStrategy;
        _bigQueryStrategy = bigQueryStrategy;
    }

    public IAdInsightsStrategy GetStrategy(bool useRealTime)
    {
        return useRealTime ? _redisStrategy : _bigQueryStrategy;
    }
}
