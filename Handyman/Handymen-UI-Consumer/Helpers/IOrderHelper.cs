using Handymen_UI_Consumer.Models;

namespace Handymen_UI_Consumer.Helpers
{
    public interface IOrderHelper
    {

        Task<List<Order>> LoadUserOrders();
    }
}