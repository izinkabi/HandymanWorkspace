using HandymanUILibrary.API.Consumer;
using Handymen_UI_Consumer.Helpers;
using Handymen_UI_Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;

namespace Handymen_UI_Consumer.ViewComponents
{
    public class OrderViewComponent : ViewComponent
    {
             private IOrderHelper _orderHelper;
             private Order order;
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

