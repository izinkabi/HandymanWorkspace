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
        private Order _order;
        public OrderDetailsModel(Handymen_UI_Consumer.Data.Handymen_UI_ConsumerContext context, 
            IOrderEndPoint orderEndPoint)
        {
            _context = context;
            _orderEndPoint = orderEndPoint; 
        }

        //The OrderModel as a class property
        [BindProperty(SupportsGet =true)]
        public Order Order { get { return _order; }  
            set 
            {
                 _order= value;
              
            }
        } 

        //This method displays the Order from the SignalR Hub method
        public IActionResult OnGetAsync(int id)
        {
           
            _order.Id = id;
            if (_order == null)
            {
                return NotFound();
            }
            ViewData["order"] = _order;

            return Page();
        }

    }
}
