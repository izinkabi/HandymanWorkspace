using System;

namespace HandymanUILibrary.Models
{
    public class HandymenDetailsModel
    {
        public int Id { get; set; }
        string bus_registrationid { get; set; }
        public string bus_name { get; set; }
        public string Names { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        byte[] Picture { get; set; } = null;
        string DeliveryRole { get; set; }


    }
}
