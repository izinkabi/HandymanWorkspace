namespace Handyman_DataLibrary.Models
{
    public class WorkshopRegistrationModel
    {
        //workiness
        public int work_Id { get; set; }
        public int work_registrationid { get; set; }
        public int Work_addressid { get; set; }
        public DateTime work_datecreated { get; set; }
        public string? work_name { get; set; }
        public string? work_description { get; set; }

        //registration

        public int reg_Id { get; set; }
        public string? reg_name { get; set; }
        public string? reg_regnumber { get; set; }
        public string? reg_taxnumber { get; set; }
        public int reg_workType { get; set; }

        //Address
        public int add_Id { get; set; }
        public string? add_street { get; set; }
        public string? add_suburb { get; set; }
        public string? add_city { get; set; }
        public string? add_zip { get; set; }
        public float add_latitude { get; set; }
        public float add_longitude { get; set; }
        public string? add_country { get; set; }
        public string? add_state { get; set; }
    }
}
