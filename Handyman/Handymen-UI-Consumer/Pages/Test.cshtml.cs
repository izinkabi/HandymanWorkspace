using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using Handymen_UI_Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages
{
    public class TestModel : PageModel
    {
        private readonly ILogger<TestModel> _logger;
    
        public TestModel(ILogger<TestModel> logger)
        {
            _logger = logger;
         
        }

        
        public async Task OnGet()
        {
            
        }
    }
}