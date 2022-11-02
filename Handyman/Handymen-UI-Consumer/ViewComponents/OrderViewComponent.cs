using HandymanUILibrary.Models;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Mvc;


namespace Handymen_UI_Consumer.ViewComponents
{
    public class OrderViewComponent : ViewComponent
    {
             private IOrderHelper _orderHelper;
             private OrderModel order;
             public OrderViewComponent(IOrderHelper orderHelper)
             {
                _orderHelper = orderHelper;
             }

             //Get the orderId to delete the order
             public async Task<IViewComponentResult> InvokeAsync(int Id) 
             {
                var o = await _orderHelper.GetOrderById(Id);

                ViewData["oid"] = Id ;
                
                return View(o);
             }



    }

}

