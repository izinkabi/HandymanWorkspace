namespace Handyman_DataLibrary.Models
{
    public class Employee_Rating_Model
    {
        public string? emp_userid { get; set; }
        public int emp_businessid { get; set; }
        public int rate_id { get; set; }
        public int rate_stars { get; set; }
        public string? rate_review { get; set; }
        public string? rate_recommendation { get; set; }

    }
}
