using HandymanDataLibray.DataAccess;
using HandymanDataLibray.Models;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
    public class ConsumersController : ApiController
    {
        private ConsumerData consumerData;

        //GET
        //api/Consumers/ProfileId
        [Route("api/Consumer/GetConsumerById")]
        public ConsumerModel GetConsumerById(string Id)
        {
            consumerData = new ConsumerData();
            return consumerData.GetConsumerById(Id);
        }

        //Post
        [Route("api/Consumer/PostConsumer")]
        public void PostConsumer(ConsumerModel consumer)
        {
            consumerData = new ConsumerData();
            consumerData.PostConsumer(consumer);
        }
    }
}
