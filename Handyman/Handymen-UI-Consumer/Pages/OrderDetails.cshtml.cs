using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Handymen_UI_Consumer.Data;
using Handymen_UI_Consumer.Models;
using HandymanUILibrary.API.Consumer;

namespace Handymen_UI_Consumer.Pages
{
    public class OrderDetailsModel : PageModel
    {
        private readonly Handymen_UI_Consumer.Data.Handymen_UI_ConsumerContext _context;
        private IOrderEndPoint _orderEndPoint;
        private Order order;
        public OrderDetailsModel(Handymen_UI_Consumer.Data.Handymen_UI_ConsumerContext context, 
            IOrderEndPoint orderEndPoint)
        {
            _context = context;
            _orderEndPoint = orderEndPoint; 
        }

        [BindProperty(SupportsGet =true)]
        public Order Order { get { return order; }  
            set 
            {
                 order= value;
              
            }
        } 

        //This method displays the Order from the SignalR Hub direct method
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _orderEndPoint == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
            }
            return Page();
        }

        public async Task OnPostAsync()
        {

        }
    }
}
