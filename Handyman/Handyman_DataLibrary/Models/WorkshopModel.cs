namespace Handyman_DataLibrary.Models;

public class WorkshopModel
{
    public int Id { get; set; }
    public RegistrationModel? registration { get; set; } = new RegistrationModel();
    public IList<MemberModel>? Employees { get; set; }
    public AddressModel? address { get; set; } = new AddressModel();
    public DateTime date { get; set; }
    public int Type { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}