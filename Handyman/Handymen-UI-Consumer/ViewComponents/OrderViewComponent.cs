using HandymanUILibrary.Models;
using Handymen_UI_Consumer.Areas.Identity.Data;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Handymen_UI_Consumer.ViewComponents
{
    public class OrderViewComponent : ViewComponent
    {
        private IOrderHelper _orderHelper;
        private OrderModel order;
        private SignInManager<Handymen_UI_ConsumerUser> SignInManager;
        public OrderViewComponent(IOrderHelper orderHelper, SignInManager<Handymen_UI_ConsumerUser> signInManager)
        {
            _orderHelper = orderHelper;
            SignInManager = signInManager;
        }

        //Get the orderId to delete the order
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var o = await _orderHelper.GetOrderById(Id, SignInManager.UserManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User));

            ViewData["oid"] = Id;

            return View(o);
        }



    }

}

