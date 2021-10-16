using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.Models
{
    public class ProvidersServiceModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ServiceProviderId { get; set; }
    }
}
