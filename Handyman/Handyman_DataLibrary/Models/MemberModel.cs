namespace Handyman_DataLibrary.Models
{
    public class MemberModel : EmployeeModel
    {
        public List<ServiceModel>? Services { get; set; }
        public string? member_profileId { get; set; }

    }
}
