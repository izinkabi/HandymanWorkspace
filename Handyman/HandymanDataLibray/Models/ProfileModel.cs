﻿/*A profile data Model*/
using System;
using System.Collections.Generic;
using System.Text;

namespace HandymanDataLibrary.Models
{
    public class ProfileModel
    {
        
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Surname { get; set; }
        public string HomeAddress { get; set; }
        public int PhoneNumber { get; set; }
        public string DateofBirth { get; set; }
        public string Type { get; set; }
    }
}
