using Handymen_UI_Consumer.Helpers;
using Handymen_UI_Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Handymen_UI_Consumer.ViewComponents
{
    public class OrderConfirmationViewComponent : ViewComponent
    {
        private IOrderHelper _orderHelper;
        private string? ErrorMsg;
        private int OrderId;
        public OrderConfirmationViewComponent(IOrderHelper orderHelper)
        {
            _orderHelper = orderHelper; 
        }


        //Displaying the deleted order
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            return View();
        }
    }
}
