using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Services;

public interface IServiceEndPoint
{
    Task<List<ServiceModel>> GetServices();
    Task<List<ServiceCategoryModel>> GetServiceCategories();

    Task<ServiceModel> GetServiceById(int id);
    Task RemoveCustom(ServiceModel service);
    Task UpdateService(ServiceModel service);
    Task<PriceModel> GetPrice(int priceId);
}