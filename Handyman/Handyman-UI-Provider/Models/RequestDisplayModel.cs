using Handyman_UI_Provider.Models.Services;
using HandymanProviderLibrary.Models;

namespace Handyman_UI_Provider.Models
{
    public class RequestDisplayModel
    {
        public string? Id { get; set; }
        public OrderModel? order { get; set; }
        ProviderServiceModel? provider { get; set; }
        //public string? ServiceProviderId { get; set; }
        public string? Stage { get; set; }

        public DisplayServiceModel? Service { get; set; }


        
    }
}
