using System.ComponentModel.DataAnnotations;

namespace Handyman_SP_UI.Models;

public class WorkshopDisplayModel
{
    public int Id { get; set; }
    public RegistrationDisplayModel? registration { get; set; }
    [Required]
    public MemberDisplayModel? Employee { get; set; }

    [Required]
    public AddressDisplayModel? address { get; set; }

    [DataType(DataType.DateTime)]
    [Required]
    [Display(Name = "Registration Date")]
    public DateTime date { get; set; }

    [DataType(DataType.Text)]
    [Required]
    [Display(Name = "Type")]
    public int Type { get; set; }

    [DataType(DataType.Text)]
    [Required]
    [Display(Name = "Title")]
    public string? Name { get; set; }

    [DataType(DataType.MultilineText)]
    [Required]
    [Display(Name = "WorkshopDisplayModel About")]
    public string? Description { get; set; }
}