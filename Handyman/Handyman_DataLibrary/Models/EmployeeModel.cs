using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handyman_DataLibrary.Models
{
    /// <summary>
    /// Employees model with a list of services, business and ratings
    /// </summary>
    public class EmployeeModel
    {
        public string? employeeId { get; set; }
        public IList<RatingsModel>? ratings { get; set; }
        public int BusinessId { get; set; }
        public string? FullName { get; set; }
        public DateTime DateEmployed { get; set; }
    }
}
