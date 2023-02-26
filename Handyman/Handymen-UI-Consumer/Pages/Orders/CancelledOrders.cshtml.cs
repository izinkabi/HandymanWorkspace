using Handymen_UI_Consumer.Areas.Identity.Data;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages.Orders;

public class CancelledOrdersModel : PageModel
{
    IOrderHelper _orderHelper;
    UserManager<Handymen_UI_ConsumerUser> UserManager;

    public CancelledOrdersModel(IOrderHelper orderHelper, UserManager<Handymen_UI_ConsumerUser> userManager)
    {
        _orderHelper = orderHelper;
        UserManager = userManager;
    }

    List<HandymanUILibrary.Models.OrderModel> orders;

    public List<HandymanUILibrary.Models.OrderModel> OrdersProperty
    {
        get { return orders; }
        private set { orders = value; }
    }

    public async Task<IActionResult> OnGet()
    {
        try
        {
            await _orderHelper.LoadUserOrders(UserManager.GetUserId(User));
            orders = _orderHelper.CancelledOrdersProperty;
            return Page();
        }
        catch (Exception)
        {

            throw;
        }

    }
}
