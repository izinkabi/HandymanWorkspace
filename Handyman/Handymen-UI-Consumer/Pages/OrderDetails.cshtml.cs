using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public OrderDetailsModel(Handymen_UI_Consumer.Data.Handymen_UI_ConsumerContext context, 
            IOrderEndPoint orderEndPoint)
        {
            _context = context;
            _orderEndPoint = orderEndPoint; 
        }

        [BindProperty(SupportsGet =true)]
        public Order Order { get; set; } 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Order == null)
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
    }
}
