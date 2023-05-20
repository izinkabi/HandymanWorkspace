using HandymanUILibrary.API.Services;
using HandymanUILibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages.Services;
[Authorize]
public class DetailsModel : PageModel
{

    private IServiceEndPoint _serviceEndPoint;
    private readonly ILogger<DetailsModel> logger;

    public DetailsModel(IServiceEndPoint serviceEndPoint, ILogger<DetailsModel> logger)
    {
        _serviceEndPoint = serviceEndPoint;
        this.logger = logger;
    }

    public ServiceModel? Service { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        logger.LogInformation("Details displayed");
        if (id == null || _serviceEndPoint == null)
        {
            return NotFound();
        }

        List<ServiceModel> services = await _serviceEndPoint.GetServices();
        foreach (var service in services)
        {
            if (service == null)
            {
                return NotFound();
            }
            else if (service.id == id)
            {
                Service = new();
                Service = service;
                logger.LogInformation("Details displayed");
                return Page();
            }

        }
        return Page();

    }
}
