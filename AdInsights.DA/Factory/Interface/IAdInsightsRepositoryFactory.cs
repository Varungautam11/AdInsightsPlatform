using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdInsights.DA.Factory.Interface;

    public interface IAdInsightsRepositoryFactory
    {
        T GetRepository<T>() where T : class;
    }

