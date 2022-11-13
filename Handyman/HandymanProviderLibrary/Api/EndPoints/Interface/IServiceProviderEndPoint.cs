using HandymanProviderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IServiceProviderEndPoint
    {
         Task AddService(ServiceProviderModel provider);
         Task RemoveService(ServiceModel service,string providerId);
         Task CreateServiceProvider(ServiceProviderModel provider);
    }
}
