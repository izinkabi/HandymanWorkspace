using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class ProfileDisplayModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        private AddressModel Address;
        //public string Type { get; }

        [DataType(DataType.Text)]
        [Display(Name = "Name(s)")]
        [Required(ErrorMessage = "Please enter your Name(s)")]
        public string Name { get; set; }

        //public AddressModel Address { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Please enter your Surname")]
        public string Surname { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Please enter your your date of birth")]
        public string DateOfBirth { get; set; }

        //[DataType(DataType.MultilineText)]
        //[Display(Name = "Home Address")]
        //[Required(ErrorMessage = "Please enter your Home Address")]
        //public string HomeAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your phone number with a contry code example(+27 ---)")]
        public string PhoneNumber { get; set; }

        public int ActivationId { get; }

        public string Type { get; set; }
        public AddressModel AddressM { get => Address; set => Address = value; }

        public class AddressModel
        {
            
            
            [Key]
            public int Id { get; set; }
            public int HouseNumber { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Street name")]
            [Required(ErrorMessage = "Please enter your the name of your street")]            
            public string StreetName { get; set; }


            [DataType(DataType.Text)]
            [Display(Name = "Surburb")]
            [Required(ErrorMessage = "Please enter your house number")]
            public string Surburb { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "City/Metro")]
            [Required(ErrorMessage = "Please enter your city name")]
            public string City { get; set; }
            [DataType(DataType.PostalCode)]
            [Display(Name = "Postal Code")]
            [Required(ErrorMessage = "Please enter your postal code")]
            public int PostalCode { get; set; }
            
        }
    }
}