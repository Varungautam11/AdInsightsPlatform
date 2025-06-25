namespace AdInsights.ServiceLibrary.Strategies.Interface;
public interface IAdInsightsStrategyfactory
{
    IAdInsightsStrategy GetStrategy(bool useRealTime);
}

