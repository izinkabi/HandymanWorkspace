using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HandymanDataLibrary.Models
{
    public class ServiceProviderModel
    {

        public int Id { get; set; }
        public string Name { get; set; }     
        public string Surname { get; set; }  
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string UserId { get; set; }

        
        
    }
}