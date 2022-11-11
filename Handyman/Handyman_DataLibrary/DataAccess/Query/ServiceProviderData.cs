using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class ServiceProviderData :  IServiceProviderData
    {
        ISQLDataAccess _dataAccess;
        public ServiceProviderData(ISQLDataAccess dataAccess) 
        {
            _dataAccess = dataAccess;
        }

        public void InsertServices(IEnumerable<IServiceProvider> services)
        {
            foreach (var service in services)
            {

            }
        }

        public void RemoveService(int serviceId)
        {

        }
    }
}
