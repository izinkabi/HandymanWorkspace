using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HandymanUILibrary.API;
using Microsoft.AspNetCore.Authorization;
using HandymanUILibrary.Models;

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

         public ServiceModel? Service { get; set; } 
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _serviceEndPoint == null)
            {
                return NotFound();
            }

            List<ServiceModel> services = await _serviceEndPoint.GetServices();
            foreach (var service in services)
            {
                if (service == null)
                {
                    return NotFound();
                }
                else if(service.id == id)   
                {
                    Service = new();
                    Service = service;
                    return Page();
                }
               
            }
            return Page();

        }
    }
}
