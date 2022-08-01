using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using Handymen_UI_Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
    
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
         
        }

        
        public async Task OnGet()
        {
            
        }
    }
}