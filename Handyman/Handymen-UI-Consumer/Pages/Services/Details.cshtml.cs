using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Handymen_UI_Consumer.Models;
using HandymanUILibrary.API;
using Microsoft.AspNetCore.Authorization;

namespace Handymen_UI_Consumer.Pages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
       
        private IServiceEndPoint _serviceEndPoint;
        public DetailsModel(IServiceEndPoint serviceEndPoint)
        {
           _serviceEndPoint = serviceEndPoint;
        }

         public Service Service { get; set; } 
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _serviceEndPoint == null)
            {
                return NotFound();
            }

            List<HandymanUILibrary.Models.ServiceModel> services = await _serviceEndPoint.GetServices();
            foreach (var service in services)
            {
                if (service == null)
                {
                    return NotFound();
                }
                else if(service.Id == id)   
                {
                    Service = new();
                    Service.Name = service.Name;
                    Service.Description = service.Description;  
                    Service.CategoryDescription = service.CategoryDescription;
                    Service.Id = service.Id;
                    Service.CategoryId = service.CategoryId;
                    Service.CategoryName = service.CategoryName;
                    Service.ImageUrl = service.ImageUrl;
                    return Page();
                }
               
            }
            return Page();

        }
    }
}
