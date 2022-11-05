using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Handymen_UI_Consumer.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;





namespace Handymen_UI_Consumer.Pages;

[Authorize]
public class OrderDetailsModel : PageModel
{
    SignInManager<Handymen_UI_ConsumerUser> _signInManager;
    HandymanUILibrary.Models.OrderModel order = new()!;
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
    public HandymanUILibrary.Models.OrderModel Order
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

        order.service = new()!;
        
        if (id == 0)
        {
            return NotFound();
        }

        try
        {
            if (_signInManager.IsSignedIn(User))
                order = await _orderHelper.GetOrderById(id.Value);
        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;
        }


        return Page();
    }

    
    public async Task<RedirectResult> OnPostAsync(int Id)
    {
        try
        {
            await _orderHelper.DeleteOrder(Id);

        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;

        }
        return Redirect("./Index");

    }
}
