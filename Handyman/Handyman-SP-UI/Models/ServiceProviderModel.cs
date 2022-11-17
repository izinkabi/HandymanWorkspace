using System.ComponentModel.DataAnnotations;

namespace Handyman_SP_UI.Models;

public class ServiceProviderModel : EmployeeDisplayModel
{

    [Required]
    public ProfileDisplayModel profile { get; set; }

    public IList<ServiceDisplayModel>? Services { get; set; }
    public string? pro_providerId { get; set; }


}
