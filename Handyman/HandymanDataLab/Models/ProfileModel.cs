using System;
using System.Collections.Generic;
using System.Text;

namespace HandymanDataLibrary.Models
{
    class ProfileModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int ActivationCode { get; set; }
        public string UserId { get; set; }
    }
}
