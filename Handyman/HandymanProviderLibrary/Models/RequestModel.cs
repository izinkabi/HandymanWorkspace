﻿namespace HandymanProviderLibrary.Models
{
    public class RequestModel
    {
        public int req_id { get; set; }
        public DateTime req_datecreated { get; set; }
        public string? req_status { get; set; }
        public int req_progress { get; set; }
        public string? req_employeeid { get; set; }
        public int req_orderid { get; set; }
        List<TaskModel>? tasks { get; set; }

    }
}
