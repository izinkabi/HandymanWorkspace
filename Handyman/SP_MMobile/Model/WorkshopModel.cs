namespace SP_MLibrary.Model;

public class WorkshopModel
{
    public int Id { get; set; }
    public RegistrationModel? registration { get; set; } = new RegistrationModel();
    public List<MemberModel>? Employees { get; set; } = new();
    public AddressModel? address { get; set; } = new AddressModel();
    public DateTime date { get; set; }
    public int Type { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}