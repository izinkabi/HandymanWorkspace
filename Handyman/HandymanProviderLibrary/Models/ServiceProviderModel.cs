namespace HandymanProviderLibrary.Models;

public class ServiceProviderModel : EmployeeModel
{
    public ProfileModel profile { get; set; }
    public IList<ServiceModel>? Services { get; set; }
    public string? pro_providerId { get; set; }


}
