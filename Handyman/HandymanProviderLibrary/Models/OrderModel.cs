using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanProviderLibrary.Models
{
    public class OrderModel
    {
        //This model will provide the attributes required to create a request and updete
        //the customer's order
        public int Id { get; set; }
        public int ServiceId { get; set; }
        //public string? ConsumerID { get; set; }//This will be used for signalr where the customer gets a direct notification of any changes      
        public string? Stage { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFinished { get; set; }
        
    }
}
