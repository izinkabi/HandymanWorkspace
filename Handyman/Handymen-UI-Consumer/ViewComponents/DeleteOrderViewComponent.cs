using Handymen_UI_Consumer.Helpers;
using Handymen_UI_Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Handymen_UI_Consumer.ViewComponents
{
    public class DeleteOrderViewComponent : ViewComponent
    {
        private IOrderHelper _orderHelper;
        private string? ErrorMsg;
        public DeleteOrderViewComponent(IOrderHelper orderHelper)
        {
            _orderHelper = orderHelper; 
        }
       

        //Displaying the deleted order
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                ViewData["err"] = ex.Message;
            }
            
      
           
            return View();
        }
    }
}
