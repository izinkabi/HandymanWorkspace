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
                
                Services = new List<ServiceModel>();//initialize the list
                for (int i = 0;i< tempServices.Count; i++)
                {
                    var tempService = new ServiceModel();// this is new everytime the for-Loop loops only, not in the foreach
                     //the following is populating the data from UILibrary
                     tempService.Id = tempServices.ElementAt(i).Id;
                    tempService.Name = tempServices.ElementAt(i).Name;
                    tempService.Description = tempServices.ElementAt(i).Description;
                    tempService.ServiceCategoryId = tempServices.ElementAt(i).ServiceCategoryId;
                    
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
                var tempCategories = await _serviceEndPoint.GetServiceCategories();//await the 
                Categories = new List<ServiceCategoryModel>();//initialize the list
                foreach (HandymanUILibrary.Models.ServiceCategoryModel serviceCategory in tempCategories)
                {
                    var tempCategory = new ServiceCategoryModel();//this is new everytime the for-Loop loops only, not in the foreach
                    //the following is populating the data from UILibrary
                    tempCategory.Id = serviceCategory.Id;
                    tempCategory.Name = serviceCategory.Name;
                    tempCategory.Description = serviceCategory.Description;
                  
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
                 
                displayCategories = new List<ServiceDisplayModel>();//initialize the list
                foreach (ServiceModel sv in Services)
                {
                    for (int i = 0; i < Categories.Count;i++)
                    {
                        if(sv.ServiceCategoryId == Categories.ElementAt(i).Id)
                        {
                            var tempServCat = new ServiceDisplayModel();//this is new everytime the for-Loop loops only, not in the foreach
                            //the following is populating the data from UILibrary
                            tempServCat.Name = String.Concat(sv.Name.Where(c => !Char.IsWhiteSpace(c)));
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