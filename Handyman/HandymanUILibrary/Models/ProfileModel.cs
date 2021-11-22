using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Surname { get; set; }
       
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public  string Type { get; set; }
        public AddressModel Address { get; set; }

        public class AddressModel
        {

            public int Id { get; set; }
            public string StreetName { get; set; }
            public int HouseNumber { get; set; }
            public string Surburb { get; set; }
            public string City { get; set; }
            public int PostalCode { get; set; }
        }
    }
}
