/*User Data Model*/
using System;
using System.Collections.Generic;
using System.Text;

namespace HandymanDataLibrary.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password  { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }


    }
}
