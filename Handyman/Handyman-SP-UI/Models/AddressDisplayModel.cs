using System.ComponentModel.DataAnnotations;

namespace Handyman_SP_UI.Models;

public class AddressDisplayModel
{

    public int add_Id { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    [StringLength(150, ErrorMessage = "Street address is too long.")]
    public string? add_street { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [StringLength(20, ErrorMessage = "Suburb name is too long.")]
    public string? add_suburb { get; set; }
    [Required]
    [DataType(DataType.Text)]
    public string? add_city { get; set; }

    [Required]
    [DataType(DataType.PostalCode)]
    [Display(Name = "Postal code")]
    public string? add_zip { get; set; }

    public float add_latitude { get; set; }

    public float add_longitude { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Country")]
    public string? add_country { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "State / Province")]
    public string? add_state { get; set; }
}