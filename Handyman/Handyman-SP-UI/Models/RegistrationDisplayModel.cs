using System.ComponentModel.DataAnnotations;

namespace Handyman_SP_UI.Models;

public class RegistrationDisplayModel
{
    public int Id { get; set; }
    [DataType(DataType.Text)]
    [Required]
    [Display(Name = "Title")]
    public string? name { get; set; }

    [DataType(DataType.Text)]
    [Required]
    [Display(Name = "Registration Number")]
    public string? regNumber { get; set; }
    [DataType(DataType.Text)]
    [Required]
    [Display(Name = "Tax Number")]
    public string? taxNumber { get; set; }
    [DataType(DataType.Text)]
    [Required]
    [Display(Name = "Business Type")]
    public int businessType { get; set; }
}