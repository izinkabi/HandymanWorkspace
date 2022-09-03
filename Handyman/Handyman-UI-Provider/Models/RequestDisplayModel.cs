using Handyman_UI_Provider.Models.Services;
using HandymanProviderLibrary.Models;

namespace Handyman_UI_Provider.Models
{
    public class RequestDisplayModel
    {
        public int? Id { get; set; }
        public OrderModel? order { get; set; }
        ProviderServiceModel? provider { get; set; }
        //public string? ServiceProviderId { get; set; }
        public string? Stage { get; set; }
        public DateTime DateAccepted { get; set; }
        public DisplayServiceModel? Service { get; set; }


        
    }
}
