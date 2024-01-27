using System.ComponentModel.DataAnnotations;

namespace HandymanProviderLibrary.Models;

public class RegistrationModel
{
    public int Id { get; set; }
    public string? name { get; set; }
    [Required]
    [Display(Name = "Registration Number")]
    [DataType(DataType.Text)]
    [StringLength(11, ErrorMessage = "Invalid Registration Number", MinimumLength = 11)]
    public string? regNumber { get; set; }
    public string? taxNumber { get; set; }
    public int workType { get; set; }
}