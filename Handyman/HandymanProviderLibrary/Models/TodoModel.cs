﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanProviderLibrary.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int OrderId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
