using System.ComponentModel.DataAnnotations;

namespace Handyman_SP_UI.Models;

public class ProfileDisplayModel
{
    [Required]
    [StringLength(150)]
    [Display(Name = "Name(s)")]
    public string? Names { get; set; }
    [Required]
    [StringLength(100)]
    [Display(Name = "Surname")]
    public string? Surname { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string? EmailAddress { get; set; }

    public int AddressId { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date Of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Phone number")]
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }
}
