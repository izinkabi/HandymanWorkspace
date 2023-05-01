﻿using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.SignalR;
namespace Handyman_SP_UI.Hubs;

public class NegotiationHub : Hub
{
    /// <summary>
    /// Server Side SendOffer 
    /// Here a sender and listener are negotiating an order's 
    /// tasks (prices and the due date) hence computing order price
    /// </summary>
    /// <param name="user"> represents the identity of the connected user</param>
    /// <param name="TaskPrices">carries the negotiated task prices, key=taskID and PriceModel</param>
    /// <param name="DueDate">negotiated due date for the order after adding a task</param>
    /// <returns></returns>

    public Task SendOffer(string user, Dictionary<int, PriceModel> TaskPrices, DateTime DueDate)
    {
        return Clients.All.SendAsync("ReceiveOffer", user, TaskPrices, DueDate);
    }

    public Task SendTasksUpdate(string user, DateTime dueDate, float total)
    {
        return Clients.All.SendAsync("ReceiveNegoUpdate", user, dueDate, total);
    }

}
