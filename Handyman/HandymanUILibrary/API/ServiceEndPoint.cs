using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class ServiceEndPoint: IServiceEndPoint
    {
        private IAPIHelper _aPIHelper;
        

        public ServiceEndPoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        public async Task<List<ServiceModel>> GetServices()
        {

           
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/GetServices"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    var result = await httpResponseMessage.Content.ReadAsAsync<List<ServiceModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

        }


        public async Task<List<ServiceCategoryModel>> GetServiceCategories()
        {


            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/api/GetServiceCategory"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    var result = await httpResponseMessage.Content.ReadAsAsync<List<ServiceCategoryModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

        }

        //Get a service by id- api
        public async Task<ServiceModel> GetServiceById(int id)
        {


            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/GetServiceById?Id={id}"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    var result = await httpResponseMessage.Content.ReadAsAsync<ServiceModel>();

                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

        }

    }
}
