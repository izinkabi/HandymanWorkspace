using HandymanDataLibray.DataAccess;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
    public class ConsumersController : ApiController
    {
        private ConsumerData consumerData;

        //GET
        //api/Consumers/ProfileId
        public ConsumerModel GetConsumerByProfileId(int profileId)
        {
            consumerData = new ConsumerData();
            return consumerData.GetConsumerByProfileId(profileId);
        }

        //Post
        public void PostConsumer(ConsumerModel consumer)
        {
            consumerData = new ConsumerData();
            consumerData.PostConsumer(consumer);
        }
    }
}
