using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class ServiceEndPoint: IServiceEndPoint
    {
        private IAPIHelper _aPIHelper;
        
        /// <summary>
        /// Constractor for the API helper
        /// </summary>
        /// <param name="aPIHelper"></param>
        /// 
        
        public ServiceEndPoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }
        /// <summary>
        /// This method is used to Get a list of Service from the API
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ServiceModel>> GetServices()
        {

            try
            {

                List<ServiceModel> httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<List<ServiceModel>>("Services/GetServies");
                return httpResponseMessage;
            }
            catch (Exception ex) 
            {
                throw  new Exception(ex.Message);  
            }
        

        }
        /// <summary>
        /// This method is used to get a list of service categories from the API
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ServiceCategoryModel>> GetServiceCategories()
        {
            try
            {
                List<ServiceCategoryModel> httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<List<ServiceCategoryModel>>("Services/GetServiceCategories");
                return httpResponseMessage;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }
        /// <summary>
        ///This method is used to Find a service using it's ID from the API
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ServiceModel> GetServiceById(int id)
        {


            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/GetServiceById?Id={id}"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    var result = await httpResponseMessage.Content.ReadFromJsonAsync<ServiceModel>();

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
