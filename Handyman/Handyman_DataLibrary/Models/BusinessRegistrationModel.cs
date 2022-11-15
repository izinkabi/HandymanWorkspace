namespace Handyman_DataLibrary.Models
{
    public class BusinessRegistrationModel
    {
        //business
        public int bus_Id { get; set; }
        public int bus_registrationid { get; set; }
        public int bus_addressid { get; set; }
        public DateTime bus_datecreated { get; set; }
        public string? bus_name { get; set; }

        //registration

        public int reg_Id { get; set; }
        public string? reg_name { get; set; }
        public string? reg_regnumber { get; set; }
        public string? reg_taxnumber { get; set; }
        public int reg_businesstype { get; set; }

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
