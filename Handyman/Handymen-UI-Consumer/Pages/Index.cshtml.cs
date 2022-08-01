﻿using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using Handymen_UI_Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IServiceEndPoint _serviceEndPoint;
        private List<Service> serviceDisplayList;
        private string? ErrorMsg;

        public IndexModel(ILogger<IndexModel> logger, IServiceEndPoint serviceEndPoint)
        {
            _logger = logger;
            _serviceEndPoint = serviceEndPoint;
        }

        [BindProperty]
        public List<Service> ServiceDisplayList
        {
            get
            {
                return serviceDisplayList;
            }

        }

        //Populating the Library's model in the UI model
        private async Task LoadServices()
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

                    serviceDisplayList.Add(service);
                }

            }catch(Exception ex)
            {
                ErrorMsg = ex.Message;
            }
           
        }

        public async Task OnGet()
        {
            await LoadServices();
        }
    }
}