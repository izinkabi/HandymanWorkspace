using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.Models
{
   public class aliasProfileModel
    {
        
            public int Id { get; set; }
            public string Name { get; set; }
            public string UserId { get; set; }
            public string Surname { get; set; }

            public string PhoneNumber { get; set; }
            public string DateofBirth { get; set; }

            public int AddressId { get; set; }
           
            public string StreetName { get; set; }
            public string HouseNumber { get; set; }
            public string Surburb { get; set; }
            public string City { get; set; }
            public int PostalCode { get; set;}
            
   }
}

