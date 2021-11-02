using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.Models
{
    public class loggedInUserModel : IloggedInUserModel
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void ResetUserModel()
        {

            Token = "";
            Id = "";
            Username = "";
            Email = "";
            CreateDate = DateTime.MinValue;

        }
    }
}
