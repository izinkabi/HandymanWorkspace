using Handymen_UI_Consumer.Areas.Identity.Data;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages;

[Authorize(Roles = "Consumer")]
public class CheckoutModel : PageModel
{
    HandymanUILibrary.Models.OrderModel orderModel;

    IOrderHelper? _orderHelper;
    SignInManager<Handymen_UI_ConsumerUser>? _signInManager;
    UserManager<Handymen_UI_ConsumerUser> _userManager;
    protected string UId;

    public CheckoutModel(IOrderHelper? orderHelper, SignInManager<Handymen_UI_ConsumerUser> signInManager, UserManager<Handymen_UI_ConsumerUser> userManager)
    {
        _orderHelper = orderHelper;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [BindProperty(SupportsGet = true)]
    public HandymanUILibrary.Models.OrderModel OrderModelProperty { get { return orderModel; } private set { orderModel = value; } }

    public async Task<IActionResult> OnGet(int id)
    {
        try
        {
            if (id != 0)
            {

                UId = await _signInManager.UserManager.GetUserIdAsync(await _userManager.GetUserAsync(User));
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
