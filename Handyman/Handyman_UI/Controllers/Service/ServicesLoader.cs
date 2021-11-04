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
        
        private IServiceEndPoint _serviceEndPoint;
        private List<ServiceCategoryModel> Categories;
        private List<ServiceModel> Services;
        private string errorMsg;
        private List<ServiceDisplayModel> displayCategories;
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

                foreach (HandymanUILibrary.Models.ServiceModel sv in tempServices)
                {
                    var tempService = new ServiceModel();
                    tempService.Id = sv.Id;
                    tempService.Name = sv.Name;
                    tempService.CategoryId = sv.CategoryId;
                    Services = new List<ServiceModel>();
                    Services.Add(tempService);//Add to the sevices list
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
                return Services;
           
        }

        //Loading the service categories 
        public async Task<List<ServiceCategoryModel>> LoadServiceCategories()
        {
            try
            {
                var tempCategories = await _serviceEndPoint.GetServiceCategories();

                foreach(HandymanUILibrary.Models.ServiceCategoryModel serviceCategory in tempCategories)
                {
                    var tempCategory = new ServiceCategoryModel();
                    tempCategory.Id = serviceCategory.Id;
                    tempCategory.Name = serviceCategory.Name;
                    tempCategory.Description = serviceCategory.Description;
                    Categories = new List<ServiceCategoryModel>();
                    Categories.Add(tempCategory);
                     
                }
               
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            return Categories;
        }

        //Merging the Services and Categories list
        public async Task<List<ServiceDisplayModel>> getDisplayServices()
        {
            try
            {
                
                 await LoadServiceCategories();
                 await LoadServices();
                var tempServCat = new ServiceDisplayModel();
                
                foreach (ServiceModel sv in Services)
                {
                    for (int i = 0; i <= Categories.Count;i++)
                    {
                        if(sv.CategoryId == Categories.ElementAt(i).Id)
                        {
                            tempServCat.Name = sv.Name;
                            tempServCat.Category = Categories.ElementAt(i).Name;
                            displayCategories.Add(tempServCat);
                        }
                    }
                        
                }
                
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            return displayCategories;
        }


    }
}