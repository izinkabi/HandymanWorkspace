namespace SP_MLibrary.Models;

public class MemberModel : EmployeeModel
{

    public List<ServiceModel>? Services { get; set; } = new();
    public string? member_profileId { get; set; }


}
