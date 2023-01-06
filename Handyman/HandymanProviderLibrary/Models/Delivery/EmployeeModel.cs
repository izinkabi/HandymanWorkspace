using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanProviderLibrary.Models.Delivery
{
    public class EmployeeModel
    {
        public string? employeeId { get; set; }
        public IList<RatingsModel>? ratings { get; set; }
    }
}
