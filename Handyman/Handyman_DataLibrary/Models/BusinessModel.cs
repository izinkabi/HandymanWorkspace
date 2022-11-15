using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary;

public class BusinessModel
{
    public int Id { get; set; }
    public RegistrationModel? registration { get; set; }
    public ServiceProviderModel? Employee { get; set; }
    public AddressModel? address { get; set; }
    public DateTime date { get; set; }
    public int Type { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}