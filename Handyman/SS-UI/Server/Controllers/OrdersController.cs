using Blazored.LocalStorage;
using HandymanUILibrary.Models;
using HandymanUILibrary.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using SS_UI.Helpers;

namespace SS_UI.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderHelper _orderHelper;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticatedUserModel _authenticatedUser;

    public OrdersController(IOrderHelper orderHelper, ILocalStorageService localStorage,
        AuthenticatedUserModel authenticatedUser)
    {
        _orderHelper = orderHelper;
        _localStorage = localStorage;
        _authenticatedUser = authenticatedUser;
    }


    [HttpGet]
    public async Task<List<OrderModel>> GetOrders()
    {
        List<OrderModel> orders = new List<OrderModel>();
        try
        {
            if (_authenticatedUser != null && _authenticatedUser.UserId != null && _authenticatedUser.Access_Token != null)
            {
                string token = await _localStorage.GetItemAsStringAsync("token");
                _authenticatedUser.Access_Token = token;
                orders = await _orderHelper.LoadUserOrders(_authenticatedUser.UserId);
            }
        }
        catch (Exception ex)
        {
            NotFound(ex.Message);
        }
        return orders;
    }
}
