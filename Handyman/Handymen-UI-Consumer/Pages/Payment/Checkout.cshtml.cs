using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages.Payment;

[Authorize(Roles = "Consumer")]
public class CheckoutModel : PageModel
{
    HandymanUILibrary.Models.OrderModel orderModel;

    private readonly IOrderHelper _orderHelper;
    private readonly AuthenticationStateProvider _authStateProvider;
    protected string? UId;

    public CheckoutModel(IOrderHelper orderHelper, AuthenticationStateProvider authStateProvider)
    {
        _orderHelper = orderHelper;
        _authStateProvider = authStateProvider;
    }
    [BindProperty(SupportsGet = true)]
    public HandymanUILibrary.Models.OrderModel OrderModelProperty { get { return orderModel; } private set { orderModel = value; } }

    public async Task<IActionResult> OnGet(int id)
    {
        try
        {
            if (id != 0)
            {

                var state = await _authStateProvider.GetAuthenticationStateAsync();
                var user = state.User;

                UId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
                if (UId != null)
                {
                    OrderModelProperty = await _orderHelper.GetOrderById(id, UId);
                    return Page();
                }
                else
                {
                    orderModel = new HandymanUILibrary.Models.OrderModel();
                }
            }



        }
        catch (Exception ex)
        {

        }
        return Page();
    }
}
