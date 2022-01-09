using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.Models
{
    public class RequestModel
    {
        public int Id { get; set; }
        public int ConsumerId { get; set; }
        public int ProvidersServiceId { get; set; }
        public string RequestStatus { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
