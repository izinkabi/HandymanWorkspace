using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.Models
{
    public class RequestModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProviderServiceID { get; set; }
        public int IsDelivered { get; set; }
       
    }
}
