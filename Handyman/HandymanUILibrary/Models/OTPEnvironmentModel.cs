using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.Models
{
   public class OTPEnvironmentModel
    {

        public object atrributes = new { otpDeliveryMobileNumber = ""};
        public string CreationDate { get; set; }
        public string Id { get; set; }//otp_id
        public bool isEnabled { get; set; }
        public string lastValidation { get; set; }
        public string methodType { get; set; }
        public string owner { get; set; }//username
    }
}
