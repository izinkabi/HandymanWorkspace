﻿using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Order.Interface
{
    public interface IOrderEndPoint
    {
        Task DeleteOrder(OrderModel order);
        Task<IList<OrderModel>> GetOrders(string customerId);
        Task<int> PostOrder(OrderModel order);
        Task UpdateOrder(OrderModel orderUpdate);
    }
}