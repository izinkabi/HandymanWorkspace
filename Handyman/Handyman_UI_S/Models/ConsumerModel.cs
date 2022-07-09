using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Handyman_UI_S.Models
{
    public class ConsumerModel
    {
        //UserId by Identity        
        public DateTimeOffset RegistrationDate { get; set; }
        public List<OrderModel> Orders { get; set; }

    }
}