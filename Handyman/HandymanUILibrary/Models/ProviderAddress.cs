using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.Models
{
    public class ProviderAddress
    {
        //This model carries data for a service provider address
        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Surburb { get; set; }
        public int HouseNumber { get; set; }
        public int Id { get; set; }
    }
}
