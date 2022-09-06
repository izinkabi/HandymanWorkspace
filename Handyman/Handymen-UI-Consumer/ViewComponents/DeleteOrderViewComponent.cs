using Handymen_UI_Consumer.Helpers;
using Handymen_UI_Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Handymen_UI_Consumer.ViewComponents
{
    public class DeleteOrderViewComponent : ViewComponent
    {
        private IOrderHelper _orderHelper;
       
        public DeleteOrderViewComponent(IOrderHelper orderHelper)
        {
            _orderHelper = orderHelper; 
        }
       

        //Displaying the deleted order
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var od =  await _orderHelper.GetOrderById(Id);
            await _orderHelper.DeleteOrder(Id);
            return View(od);
        }
    }
}
