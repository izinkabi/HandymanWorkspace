using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class ServiceData : IServiceData
    {
        ISQLDataAccess _dataAccess;
        public ServiceData(ISQLDataAccess dataAccess)
        {
            _dataAccess = dataAccess;   
        }

        public List<ServiceModel> GetAllServices()
        {
           

            var output = _dataAccess.LoadData<ServiceModel, dynamic>("Service.spServiceLookUp_GroupByCategory", new { }, "HandymanDB");

            return output;

        }
    }
}
