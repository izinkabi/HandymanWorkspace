using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanProviderLibrary.Models;

public class ServiceProviderModel: EmployeeModel
{
 
    public IList<ServiceModel>? Services { get; set; }
    public string? pro_providerId { get; set; }

   
}
