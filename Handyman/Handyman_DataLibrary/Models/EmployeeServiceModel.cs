using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handyman_DataLibrary.Models
{
    public class EmployeeServiceModel
    {
        public string employeeId { get; set; }
        public IList<ServiceModel> myservices { get; set; }
    }
}
