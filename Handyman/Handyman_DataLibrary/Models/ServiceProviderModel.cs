namespace Handyman_DataLibrary.Models
{
    public class ServiceProviderModel : EmployeeModel
    {

        public List<ServiceModel>? Services { get; set; }
        public string? pro_providerId { get; set; }


    }
}
