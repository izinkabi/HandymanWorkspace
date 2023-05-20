using HandymanUILibrary.Models;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Handymen_UI_Consumer.ViewComponents
{
    public class OrderViewComponent : ViewComponent
    {
        private IOrderHelper _orderHelper;
        private readonly AuthenticationStateProvider _authStateProvider;
        private OrderModel order;
        public OrderViewComponent(IOrderHelper orderHelper, AuthenticationStateProvider authenticationStateProvider)
        {
            _orderHelper = orderHelper;
            _authStateProvider = authenticationStateProvider;
        }

        //Get the orderId to delete the order
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            string? userId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
            var o = await _orderHelper.GetOrderById(Id, userId);

            ViewData["oid"] = Id;

            return View(o);
        }



    }

}

