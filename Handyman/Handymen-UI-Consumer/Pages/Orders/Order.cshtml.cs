using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;

namespace Handymen_UI_Consumer.Pages
{

    public class OrderModel : PageModel
    {
      
         HandymanUILibrary.Models.OrderModel order;
         IServiceEndPoint _serviceEndPoint;
         string ErrorMsg;
         IList<ServiceModel> serviceDisplayList;
        
       
        //Injecting the services 
        public OrderModel(IServiceEndPoint serviceEndPoint)
        {
            _serviceEndPoint = serviceEndPoint;
        }

        //public Order Order { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public HandymanUILibrary.Models.OrderModel? OrderProperty
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

        
        //Get the order for display (the default method on OrderPage redirect or call)
        public async Task<IActionResult> OnGetAsync(int? id)
        {

             await buildOrder(id);

            if (order.Id == id)
            {
                return RedirectToPage("./Orders/OrderDetails");
            }
            

            return Page();
        }
      
        //Load / Create the Order 
        async Task buildOrder(int? id)
        {
            await LoadServices();

            foreach (var service in serviceDisplayList)
            {
                if (service.id == id.Value)
                {
                    order = new HandymanUILibrary.Models.OrderModel();
                    order.service = new();
                    order.service = service;
                    
                }
            }
            
        }
        //Load Services from the IServiceEndPoint service
        async Task LoadServices()
        {
           
            try
            {
                //await the endpoint
                serviceDisplayList = await _serviceEndPoint.GetServices();
             
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

        }
        //Cancelling the order 
        private void CancelOrder()
        {
            //implementation
        }

        
    }
}