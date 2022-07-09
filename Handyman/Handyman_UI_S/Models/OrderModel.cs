using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Handyman_UI_S.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string ConsumerID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Stage { get; set; }

    }
}