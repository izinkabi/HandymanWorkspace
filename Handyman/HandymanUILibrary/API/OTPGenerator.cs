using HandymanUILibrary.Models;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class OTPGenerator
    {

        private HttpClient _apiClient;
        static private string Username, OTP_id;
        
        //constructor
        public OTPGenerator()
        {
            InitializeCLient();
            _apiClient = new HttpClient();
        }
        //Initialize the apiClient
        private void InitializeCLient()
        {
            //string api = ConfigurationManager.AppSettings["api"];

            _apiClient = new HttpClient();
          //  _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //Create the OTP Environment
        public async Task<OTPEnvironmentModel> CreateOTPEnvironment(string userId, string phonenumber, string username)
        {
            var data = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("owner",userId),
                new KeyValuePair<string, string>("isEnabled", "true"),
                new KeyValuePair<string, string>( "attributes", "otpDeliveryMobileNumber: "+phonenumber
                )

            });

            //var data = new 
            //{
            //    Owner = userId,
            //    isEnabled = "true",
            //    attributes = new { otpDeliveryMobileNumber = phonenumber }

            //};
            Username = username;

            using (HttpResponseMessage httpResponse = await _apiClient.PostAsync("https://" + username + "/v1.0/authnmethods/smsotp", data))
            {
              
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsAsync<OTPEnvironmentModel>();
                    OTP_id = result.lastValidation;
                    return result;
                }
                else
                {
                    throw new Exception(httpResponse.ReasonPhrase);
                }
            }

        }

        //Validate the OTP sent on the phone 
        public async Task<OTPResult> ValidateOTP(string otp)
        {
           
            var OTP = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("otp",otp)
               
            });


            using (HttpResponseMessage httpResponse = await _apiClient.PostAsync("https://"+Username+"/v1.0/authnmethods/smsotp/"+OTP_id+"/validator/{{validator_id}}", OTP))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                   // var responseMsg = new { messageId = "", messageDescription = "" };
                    var result = await httpResponse.Content.ReadAsAsync<OTPResult>();
                    return result;
                }
                else
                {
                    throw new Exception(httpResponse.ReasonPhrase);
                }
            }

        }

    }
}
