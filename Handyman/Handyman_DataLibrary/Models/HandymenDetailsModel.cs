namespace Handyman_DataLibrary.Models
{
    public class HandymenDetailsModel
    {
        //p.Names , p.Surname, p.DateOfBirth, p.Gender, p.PhoneNumber,
        //    b.bus_name,b.bus_registrationid

        int Id { get; set; }
        string bus_registrationid { get; set; }
        public string? bus_name { get; set; }
        public string Names { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        byte[] Picture { get; set; } = null;
        string DeliveryRole { get; set; }
    }
}
