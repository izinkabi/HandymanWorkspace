using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class ConsumerEndPoint : IConsumerEndPoint
    {


        private IAPIHelper _aPIHelper;
       
        public ConsumerEndPoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }


        //Post A consumer
        public async Task<ConsumerModel> PostConsumer(ConsumerModel consumer)
        {


            HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/Consumer/PostConsumer", consumer);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadFromJsonAsync<ConsumerModel>();
                return result;
            }
            else
            {
                throw new Exception(responseMessage.ReasonPhrase);
            }
        }

        //Get a customer using the Identity UserId
        public async Task<ConsumerModel> GetConsumerById(string Id)
        {


            ConsumerModel httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<ConsumerModel>($"/api/Consumer/GetConsumerById?Id={Id}");
           /* {
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    var result = await httpResponseMessage.Content.ReadFromJsonAsync<ConsumerModel>();
                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }*/
                return httpResponseMessage;
            }
        }
    }

