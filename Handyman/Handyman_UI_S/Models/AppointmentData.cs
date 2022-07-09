using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Handyman_UI_S.Models
{
    public class AppointmentData
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean IsAllDay { get; set; }
    }
}