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
        public int ConsumerId { get; set; }
        public int ProvidersServiceId { get; set; }
        public string RequestStatus { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset FinishTime { get; set; }
    }
}
