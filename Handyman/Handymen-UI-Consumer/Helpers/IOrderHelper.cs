﻿

using HandymanUILibrary.Models;

namespace Handymen_UI_Consumer.Helpers
{
    public interface IOrderHelper
    {

        Task<List<OrderModel>> LoadUserOrders(string userId);
        Task<OrderModel> GetOrderById(int id, string userId);
        Task DeleteOrder(OrderModel order);
        Task CreateOrder(OrderModel newOrder);

    }
}