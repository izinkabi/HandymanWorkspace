using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HandymanUILibrary.Models
{
    public class OrderModel 
    {
        public int Id { get; set; }
        public string ConsumerID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Stage { get; set; }

      
    }
}

    


