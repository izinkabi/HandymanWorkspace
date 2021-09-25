using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.Models
{
    class loggedInUserModel : IloggedInUserModel
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }

        public void ResetUserModel()
        {

            Token = "";
            UserId = "";
            Username = "";
            Email = "";
            CreateDate = DateTime.MinValue;

        }
    }
}
