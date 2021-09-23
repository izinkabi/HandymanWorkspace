/*A profile data Model*/
using System;
using System.Collections.Generic;
using System.Text;

namespace HandymanDataLibrary.Models
{
    public class ProfileModel
    {
        public int Id { get;}
        public string Type { get; set; }
        public string UserId { get; set; }
    }
}
