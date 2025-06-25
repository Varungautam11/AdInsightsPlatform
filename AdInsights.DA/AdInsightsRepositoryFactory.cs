using AdInsights.DA.Factory.Interface;
using AdInsights.DA.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdInsights.DA;

internal class AdInsightsRepositoryFactory : IAdInsightsRepositoryFactory
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
        throw new NotImplementedException();
    }
}

