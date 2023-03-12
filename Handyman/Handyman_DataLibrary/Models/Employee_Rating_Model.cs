namespace Handyman_DataLibrary.Models
{
    public class Employee_Rating_Model
    {
        public string? emp_userid { get; set; }
        public int emp_businessid { get; set; }

        public DateTime emp_date_employed { get; set; }
        public int emp_role { get; set; }


        public string? Names { get; set; }
        public string? Surname { get; set; }
        public string? EmailAddress { get; set; }
        public int AddressId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? userId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }

    }
}
