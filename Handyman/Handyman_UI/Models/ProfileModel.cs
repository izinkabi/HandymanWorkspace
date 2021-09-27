using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class ProfileModel
    {
        public int Id { get; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateTimeOfBirth { get; set; }
        public string HomeAddress { get; set; }
        public int PhoneNumber { get; set; }
        public int ActivationId { get; set; }

    }
}