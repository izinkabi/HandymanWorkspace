using System;
using System.Collections.Generic;

namespace HandymanUILibrary.Models
{
    //An order model with tasks model
    public class OrderModel
    {
        public string? ConsumerID { get; set; }
        public DateTime datecreated { get; set; }
        public int status { get; set; }
        public DateTime duedate { get; set; }
        public ServiceModel? service { get; set; }
        public IList<TaskModel>? Tasks { get; set; }
        public int Id { get; set; }
        public HandymenDetailsModel Provider { get; set; }
    }
}
