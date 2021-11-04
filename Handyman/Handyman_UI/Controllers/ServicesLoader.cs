using Handyman_UI.Models;
using HandymanUILibrary.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Handyman_UI.Controllers
{
    public class ServicesLoader: IServicesLoader
    {
        private List<ServiceModel> Services;
        private IServiceEndPoint _serviceEndPoint;
        private string errorMsg;
        private List<ServiceCategoryModel> categories;


        public string _errorMsg { 
            get { return errorMsg; } 
        }


        public ServicesLoader(IServiceEndPoint serviceEndPoint)
        {
            _serviceEndPoint = serviceEndPoint;

        }


        //Load/populate services from the database to the local list
        public async Task<List<ServiceModel>> LoadServices()
        {
            try
            {

                var tempServices = await _serviceEndPoint.GetServices();
                
                foreach(HandymanUILibrary.Models.ServiceModel sv in tempServices)
                {
                    var tempService = new ServiceModel();
                    tempService.Id = sv.Id;
                    tempService.Name = sv.Name;
                    tempService.CategoryId = sv.CategoryId;

                    Services.Add(tempService);//Add to the sevices list
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.Message;
            }
            return Services;
        }

        //Loading the service categories 
        public void LoadServiceCategories()
        {
            try
            {

            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
        }
    }
}