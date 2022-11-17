using System.ComponentModel.DataAnnotations;

namespace Handyman_SP_UI.Models;

public class ServiceDisplayModel
{

    public int id { get; set; }

    [DataType(DataType.Text)]
    [Required]
    [Display(Name = "Title")]
    public string? name { get; set; }
    public string? img { get; set; }

    [Required]
    public ServiceCategoryDisplayModel? category { get; set; }

    [DataType(DataType.Date)]
    [Required]
    [Display(Name = "Date available")]
    public DateTime datecreated { get; set; }

    [DataType(DataType.Text)]
    [Required]
    [Display(Name = "Status")]
    public string? status { get; set; }
}
