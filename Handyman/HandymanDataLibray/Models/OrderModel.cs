using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ConsumerID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Stage { get; set; }
        public DateTime DateFinished { get; set; }
        public int IsAccepted { get; set; }

    }
}
