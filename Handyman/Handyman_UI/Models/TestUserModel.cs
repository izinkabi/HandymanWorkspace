using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class TestUserModel
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
    }
}