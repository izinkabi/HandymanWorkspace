using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages
{
    public class RequestMetadata
    {
        public string? Name { get; set; }
        public Dictionary<string, RequestModel> Parameters { get; set; } =
            new Dictionary<string, RequestModel>();
    }
}
