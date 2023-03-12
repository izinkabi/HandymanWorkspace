namespace HandymanProviderLibrary.Models;

public class ServiceProviderModel : EmployeeModel
{

    public List<ServiceModel>? Services { get; set; } = new();
    public string? pro_providerId { get; set; }


}
