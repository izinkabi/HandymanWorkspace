using System.ComponentModel.DataAnnotations;

namespace Handyman_SP_UI.Models;

public class ServiceCategoryDisplayModel
{
    [DataType(DataType.Text)]
    [Required]
    [Display(Name = "Title")]
    public string name { get; set; }
    [Required]
    [Display(Name = "Service Type")]
    [DataType(DataType.Text)]
    public string type { get; set; }
    [DataType(DataType.MultilineText)]
    [Required]
    [Display(Name = "Service Category Description")]
    public string description { get; set; }
}
