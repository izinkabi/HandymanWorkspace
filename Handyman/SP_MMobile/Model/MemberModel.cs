namespace SP_MMobile.Model;

public class MemberModel : EmployeeModel
{

    public List<ServiceModel>? Services { get; set; } = new();
    public string? member_profileId { get; set; }


}
