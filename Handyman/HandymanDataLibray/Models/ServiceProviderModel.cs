using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.Models
{
    public class ServiceProviderModel
    {
        public int Id { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
        public string CompanyName { get; set; }
        public string ProviderType { get; set; }
    }
}
