using System.ComponentModel.DataAnnotations;

namespace Handyman_SP_UI.Models;

public class MemberDisplayModel : EmployeeDisplayModel
{

    [Required]
    public ProfileDisplayModel profile { get; set; }
    public IList<ServiceDisplayModel>? Services { get; set; }


}
