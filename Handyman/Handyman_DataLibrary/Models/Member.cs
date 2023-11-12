namespace Handyman_DataLibrary.Models
{
    public class Member : EmployeeModel
    {
        public List<ServiceModel>? Services { get; set; }
        public string? member_profileId { get; set; }

    }
}
