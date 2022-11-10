﻿namespace Handyman_DataLibrary.Models
{
    public class BusinessModel
    {
        public Guid Id { get; set; }
        public RegistrationModel? registration { get; set; }
        public AddressModel? address { get; set; }
        public DateTime date { get; set; } 
    }
}