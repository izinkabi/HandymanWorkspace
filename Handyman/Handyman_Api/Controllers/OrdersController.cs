using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;


namespace Handyman_Api.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    IOrderData _orderData;
    IEnumerable<OrderModel>? orders;
    public OrdersController(IOrderData orderData)
    {
        _orderData = orderData;
    }
    // GET: api/<OrdersController>
    [HttpGet]
    [Route("GetOrders")]
    public IEnumerable<OrderModel> Get(string consumerId)
    {
        try
        {
            if (consumerId != null)
                orders = _orderData.GetOrders(consumerId);
            return orders;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    // POST api/<OrdersController>
    [HttpPost]
    [Route("Post")]
    public void Post(OrderModel order)
    {
        try
        {
            if (order == null)
            {
                return;
            }
            _orderData.InsertOrder(order);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // PUT api/<OrdersController>/5
    [HttpPut]
    [Route("Update")]
    public void Update(OrderModel orderUpdate)
    {
        try
        {
            if (orderUpdate == null)
            {
                return;
            }
            _orderData.UpdateOrder(orderUpdate);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // DELETE api/<OrdersController>/5
    [HttpDelete]
    public void Delete(string consumerId, int orderId)
    {
        try
        {
            _orderData.DeleteOrderAndTasks(consumerId, orderId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
}
