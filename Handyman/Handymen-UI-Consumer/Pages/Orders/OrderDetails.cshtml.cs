using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Authorization;
using HandymanUILibrary.API.Consumer.Order.Interface;


namespace Handymen_UI_Consumer.Pages
{
    [Authorize]
    public class OrderDetailsModel : PageModel
    {

        HandymanUILibrary.Models.OrderModel order = new()!;
        IOrderHelper? _orderHelper;
        string? ErrorMsg;
      

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

        //This method displays the Order from the SignalR Hub method
        public async Task<IActionResult> OnGetAsync(int Id)
        {
           

            if (Id == null)
            {
                return NotFound();
            }

            try
            {
                order = await _orderHelper.GetOrderById(Id);
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
}
