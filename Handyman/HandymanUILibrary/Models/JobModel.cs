using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.Models
{
    public class JobModel
    {
        public int JobID { get; set; }
        public string Title { get; set; }
        public string JobPosition { get; set; }
        public int CategoryID { get; set; }
        public string JobImage { get; set; }
    }
}
