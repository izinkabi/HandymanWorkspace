using Handyman_UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Handyman_UI.Controllers
{
    public interface IServicesLoader
    {
        Task<List<ServiceDisplayModel>> getDisplayServices();
    }
      
}