using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Handymen_UI_Consumer.Data;
using Handymen_UI_Consumer.Models;
using HandymanUILibrary.API;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Handymen_UI_Consumer.Pages
{
    public class IndexPageModel : PageModel
    {
        private readonly Handymen_UI_Consumer.Data.Handymen_UI_ConsumerContext _context;
        private readonly ILogger<TestModel> _logger;
        private IServiceEndPoint _serviceEndPoint;
        private List<Service>? serviceDisplayList;
        internal string? ErrorMsg;

        [BindProperty(SupportsGet = true)]
        public string? searchString { get; set; }   
        public SelectList? serviceCategorySelectList { get; set; }
        public List<string> serviceCategories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? category { get; set; }
       
        public IndexPageModel(Handymen_UI_Consumer.Data.Handymen_UI_ConsumerContext context, IServiceEndPoint serviceEndPoint)
        {
            _context = context;
            _serviceEndPoint = serviceEndPoint;
        }

       //The only method runing on ServicesHome soon to be index
        public async Task OnGetAsync()
        {
            if (serviceCategories == null || serviceDisplayList == null)
            {
                await LoadServices();
                await LoadServiceCategories();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                serviceDisplayList = serviceDisplayList.Where(s => s.Name.ToLower()!.Contains(searchString.ToLower())).ToList();
            }

            //To enable the searchByCategory funtionality uncomment this below and same in HTML page
            //if (!string.IsNullOrEmpty(category))
            //{
            //    serviceDisplayList = serviceDisplayList.Where(x => x.CategoryName == category).ToList();
            //}
            //Populate the select list, could have been done so easily
            serviceCategorySelectList = new SelectList(serviceCategories);
        }

        [BindProperty(SupportsGet =true)]
        public List<Service> ServiceList { get
            {
                return serviceDisplayList;
            }
        }

        //Load the services from UILibrary then populate
        public async Task LoadServices()
        {
            serviceDisplayList = new List<Service>();
            List<HandymanUILibrary.Models.ServiceModel> services = new List<HandymanUILibrary.Models.ServiceModel>();

            try
            {
                //await the endpoint
                services = await _serviceEndPoint.GetServices();

                foreach (var s in services)
                {
                    Service service = new Service();
                    //population
                    service.Name = s.Name;
                    service.Description = s.Description;
                    service.Id = s.Id;
                    //...category
                    service.CategoryId = s.CategoryId;
                    service.CategoryName = s.CategoryName;
                    service.CategoryDescription = s.CategoryDescription;
                    service.CategoryType = s.Type;
                    service.ImageUrl = s.ImageUrl;

                    serviceDisplayList.Add(service);
                    ErrorMsg = null;
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

        }
        
        private async Task LoadServiceCategories()
        {
            try
            {
                List<HandymanUILibrary.Models.ServiceCategoryModel> serviceCategoryModel = new();
                serviceCategories = new List<string>();
                serviceCategoryModel = await _serviceEndPoint.GetServiceCategories();
                foreach (var serviceCategory in serviceCategoryModel)
                {
                    var uiCategory = new ServiceCategory();
                    uiCategory.CategoryName = serviceCategory.CategoryName;
                    uiCategory.CategoryDescription = serviceCategory.CategoryDescription;
                    uiCategory.CategoryId = serviceCategory.CategoryId;
                    if(serviceDisplayList.Count > 0)
                    {
                        uiCategory.Services = serviceDisplayList;
                    }
                   serviceCategories.Add(uiCategory.CategoryName);
                }
                serviceCategorySelectList = new SelectList(serviceCategories);
                ErrorMsg = null;
            }catch(Exception ex)
            {
                ErrorMsg = ex.Message;           
            }
        }

      
    }
}
