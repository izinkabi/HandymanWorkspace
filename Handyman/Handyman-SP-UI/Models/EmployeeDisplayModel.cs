using System.ComponentModel.DataAnnotations;

namespace Handyman_SP_UI.Models;

/// <summary>
/// Employees model with a list of services, business and ratings
/// </summary>
public class EmployeeDisplayModel
{
    protected string? employeeId { get; set; }
    public IList<RatingsDisplayModel>? ratings { get; set; }
    protected int workId { get; set; }

    [DataType(DataType.DateTime)]
    [Required]
    [Display(Name = "Employment Date")]
    public DateTime DateEmployed { get; set; }
}
