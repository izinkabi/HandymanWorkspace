using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handyman_DataLibrary.Models
{
    public class TaskModel
    {
        public DateTime tas_date_started { get; set; }
        public DateTime tas_date_finished { get; set; }
        public int tas_duration { get; set; }
        public int task_id { get; set; }
        public string? tas_title { get; set; }
    }
}
