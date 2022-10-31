
using System;
using System.Collections.Generic;

namespace HandymanUILibrary.Models
{
    //An order model with tasks model
    public class OrderModel
    {
        public string ConsumerID { get; set; }
        public DateTime ord_datecreated { get; set; }
        public int ord_status { get; set; }
        public DateTime ord_duedate { get; set; }
        public int ord_service_id { get; set; }
        public IEnumerable<TaskModel>? Tasks { get; set; }
        public int Id { get; set; }
    }
}
