using Handymen_UI_Consumer.Models;

namespace Handymen_UI_Consumer.Helpers
{
    public class OrderHelper: IOrderHelper
    {
        private Order? orderModel = new()!;


        public Order ConfirmOrder(Order order)
        {
            if (!(order.IsConfirmed))
            {
                return new Order();
            }
            return orderModel= order;
            
        }


    }
}
