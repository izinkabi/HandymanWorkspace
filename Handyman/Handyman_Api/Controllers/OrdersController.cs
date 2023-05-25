using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Handyman_Api.Controllers;

[Route("api/orders")]
[ApiController]
[Authorize(Roles = "Consumer,Member,Owner")]
public class OrdersController : ControllerBase
{
    IOrderData _orderData;
    private readonly SignInManager<IdentityUser> _signInManager;
    IEnumerable<OrderModel>? orders;
    public OrdersController(IOrderData orderData, SignInManager<IdentityUser> signInManager)
    {
        _orderData = orderData;
        _signInManager = signInManager;
    }
    // GET: api/<OrdersController>
    [HttpGet]
    [Route("GetOrders")]
    public IEnumerable<OrderModel> Get()
    {
        try
        {

            var user = _signInManager.Context.User;
            var id = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
            if (id != null)
                orders = _orderData.GetOrders(id);
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
    public int Post(OrderModel order)
    {
        try
        {
            if (order == null)
            {
                return 0;
            }
            return _orderData.InsertOrder(order);
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
