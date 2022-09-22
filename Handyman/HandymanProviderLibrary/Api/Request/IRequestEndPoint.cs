﻿using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.Request
{
    public interface IRequestEndPoint
    {
        Task<List<OrderModel>> GetAllOrdersByService(int serviceId);
        Task<string> PostRequest(RequestModel request);
        Task<string> UpdateRequest(RequestModel updateRequest);
        Task<List<RequestModel>> GetRequestsByProvider(string? providerId);
        Task<List<TodoModel>> GetOrderTodoList(int Id);
        Task<TodoModel> GetTodoItemById(int Id);
        Task UpdateTodoItem(TodoModel todoItemUpdate);
    }
}