using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.Models
{
    internal class JobOfferModel
    {
        public int Id { get; set; }
        public string ConsumerId { get; set; }
        public int IsDelivered { get; set; }
        public int ProviderServiceId { get; set; }
    }
}
