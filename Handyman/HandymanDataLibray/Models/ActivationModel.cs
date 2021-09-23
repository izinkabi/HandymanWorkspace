/*Activation Data Model*/
using System;
using System.Collections.Generic;
using System.Text;

namespace HandymanDataLibrary.Models
{
    class ActivationModel
    {
        public int Id { get;}
        public string ActivationCode { get; set; }
        public string OTP { get; set; }
    }
}
