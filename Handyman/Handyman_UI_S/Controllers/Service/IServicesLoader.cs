using Handyman_UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Handyman_UI.Controllers
{
    public interface IServicesLoader
    {
        Task<List<ServiceModel>> LoadServices();
        Task<List<ServiceDisplayModel>> getDisplayServices();
        Task<List<ServiceCategoryModel>> LoadServiceCategories();
        Task<ServiceDisplayModel> getServiceById(int id);
    }
      
}