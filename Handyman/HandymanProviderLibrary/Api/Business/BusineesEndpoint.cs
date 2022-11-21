using HandymanProviderLibrary.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HandymanProviderLibrary.Api.Business
{
    public class BusineesEndpoint
    {
        IAPIHelper _apiHelper;

        public BusineesEndpoint(IAPIHelper apiHelper)
        {
            this._apiHelper = apiHelper;
        }

    }
}
