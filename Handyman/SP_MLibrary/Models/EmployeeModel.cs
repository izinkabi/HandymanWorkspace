namespace SP_MLibrary.Models;

/// <summary>
/// Employees model with a list of services, business and ratings
/// </summary>
public class EmployeeModel
{
    public string? employeeId { get; set; }
    public IList<RatingsModel>? ratings { get; set; }
    public int WorkId { get; set; }
    public DateTime DateEmployed { get; set; }
    public ProfileModel? employeeProfile { get; set; } = new ProfileModel();
    public bool IsOwner { get; set; }
}

