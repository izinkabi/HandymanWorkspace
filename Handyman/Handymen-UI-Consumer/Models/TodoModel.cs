﻿namespace Handymen_UI_Consumer.Models
{
    public class taskModel
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public int OrderId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
    }
}
