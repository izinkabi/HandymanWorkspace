using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IServiceData
    {
        List<ServiceModel> GetAllServices();
    }
}