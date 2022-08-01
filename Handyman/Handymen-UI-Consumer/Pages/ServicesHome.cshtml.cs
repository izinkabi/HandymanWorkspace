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

namespace Handymen_UI_Consumer.Pages
{
    public class ServicesHomeModel : PageModel
    {
        private readonly Handymen_UI_Consumer.Data.Handymen_UI_ConsumerContext _context;
        private readonly ILogger<IndexModel> _logger;
        private IServiceEndPoint _serviceEndPoint;
        private List<Service> serviceDisplayList;
        public string? ErrorMsg;

        public ServicesHomeModel(Handymen_UI_Consumer.Data.Handymen_UI_ConsumerContext context, IServiceEndPoint serviceEndPoint)
        {
            _context = context;
            _serviceEndPoint = serviceEndPoint;
        }

        public IList<Service> ServiceList { get
            {
                return serviceDisplayList;
            }
        }

        //Load the services from UILibrary then populate
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
                    service.ImageUrl = s.ImageUrl;

                    serviceDisplayList.Add(service);
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

        }
        public async Task OnGetAsync()
        {
            if(ServiceList is null)
            {
                await LoadServices();
            }
           
            //if (_context.Service != null)
            //{
            //    ServiceList = await _context.Service.ToListAsync();
            //}
        }
    }
}
