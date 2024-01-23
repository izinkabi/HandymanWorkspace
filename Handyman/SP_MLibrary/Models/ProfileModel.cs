namespace SP_MLibrary.Models;

public class ProfileModel
{

    public string? Names { get; set; }
    public string? Surname { get; set; }
    public string? EmailAddress { get; set; }
    public AddressModel Address { get; set; } = new AddressModel();
    public DateTime DateOfBirth { get; set; }
    public string? UserId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Gender { get; set; }
    public string? ImageUrl { get; set; }

}
