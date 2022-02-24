using Handyman_UI.Models;
using System.Collections.Generic;

namespace Handyman_UI.Controllers.Customer.Helpers
{
    public interface ICustomerHelper
    {
        void ActionARequest(CustomerModel customer, ServiceModel service);
    }
}