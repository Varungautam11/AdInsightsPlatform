using AdInsights.DA.Factory.Interface;
using AdInsights.DA.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdInsights.DA.Factory;

public class AdInsightsRepositoryFactory : IAdInsightsRepositoryFactory
{
    private readonly IRedisAdInsightsRepository _redisRepo;
    private readonly IBigQueryAdInsightsRepository _bigQueryRepo;

    public AdInsightsRepositoryFactory(IRedisAdInsightsRepository redisRepo, IBigQueryAdInsightsRepository bigQueryRepo)
    {
        _redisRepo = redisRepo;
        _bigQueryRepo = bigQueryRepo;
    }

    public T GetRepository<T>() where T : class
    {
        if (typeof(T) == typeof(IRedisAdInsightsRepository))
            return _redisRepo as T;

        if (typeof(T) == typeof(IBigQueryAdInsightsRepository))
            return _bigQueryRepo as T;

        throw new InvalidOperationException("Unsupported repository type.");
    }
}

