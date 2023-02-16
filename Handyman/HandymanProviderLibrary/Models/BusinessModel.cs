namespace HandymanProviderLibrary.Models;

public class BusinessModel
{
    public int Id { get; set; }
    public RegistrationModel? registration { get; set; } = new RegistrationModel();
    public ServiceProviderModel? Employee { get; set; } = new ServiceProviderModel();
    public AddressModel? address { get; set; } = new AddressModel();
    public DateTime date { get; set; }
    public int Type { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}