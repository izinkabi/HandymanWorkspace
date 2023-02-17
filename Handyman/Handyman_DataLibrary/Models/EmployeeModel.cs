namespace Handyman_DataLibrary.Models;

/// <summary>
/// Employees model with a list of services, business and ratings
/// </summary>
public class EmployeeModel
{
    public string? employeeId { get; set; }
    public IList<RatingsModel>? ratings { get; set; }
    public int BusinessId { get; set; }
    public DateTime DateEmployed { get; set; }
    public ProfileModel? employeeProfile { get; set; } = new ProfileModel()!;
}
