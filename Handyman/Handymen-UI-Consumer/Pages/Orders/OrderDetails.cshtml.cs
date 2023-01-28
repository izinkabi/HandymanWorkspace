using Handymen_UI_Consumer.Areas.Identity.Data;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages;

[Authorize]
public class OrderDetailsModel : PageModel
{
    SignInManager<Handymen_UI_ConsumerUser> _signInManager;
    HandymanUILibrary.Models.OrderModel order;
    IOrderHelper? _orderHelper;
    string? ErrorMsg;

    public OrderDetailsModel(IOrderHelper orderHelper,
        SignInManager<Handymen_UI_ConsumerUser> signInManager)
    {
        _orderHelper = orderHelper;
        _signInManager = signInManager;
    }

    //The OrderModel as a class property
    [BindProperty(SupportsGet = true)]
    public HandymanUILibrary.Models.OrderModel OrderProperty
    {
        get { return order; }
        set
        {
            order = value;

        }
    }


    //Get on Page redirection and 
    public async Task<IActionResult> OnGetAsync(int? id)
    {

        //order.service = new()!;

        if (id == null)
        {
            return NotFound();
        }

        try
        {

            order = await _orderHelper.GetOrderById(id.Value, _signInManager.UserManager.GetUserId(User));

        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;
        }


        return Page();
    }


    public async Task<RedirectResult> OnPostAsync()
    {
        try
        {
            await _orderHelper.DeleteOrder(order);
        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;
        }
        return Redirect("./Index");

    }
}
