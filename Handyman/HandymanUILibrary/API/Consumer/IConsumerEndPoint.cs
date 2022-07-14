using HandymanUILibrary.Models;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public interface IConsumerEndPoint
    {
        Task<ConsumerModel> PostConsumer(ConsumerModel consumer);
        Task<ConsumerModel> GetConsumerById(string Id);
        Task<ConsumerModel> GetConsumerByProfileId(int id);
    }
}