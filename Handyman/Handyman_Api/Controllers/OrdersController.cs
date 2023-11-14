using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Handyman_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    IOrderData _orderData;


    public OrdersController(IOrderData orderData)
    {
        _orderData = orderData;
        //_orderData = orderData;
    }


    // GET: api/<ordersController>/5
    [HttpGet]
    [Route("Getorder")]
    public OrderModel Getorder(int Id)
    {
        try
        {
            OrderModel neworder = _orderData.GetOrder(Id);
            return neworder;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //--------Filtering Methods
    [HttpGet]
    [Route("GetCurrentWeek")]
    public IList<OrderModel> GetCurrentWeekorders(string empID) => _orderData.GetCurrentWeekOrders(empID);
    [HttpGet]
    [Route("GetCurrentMonth")]
    public IList<OrderModel> GetCurrentMonthorders(string empID) => _orderData.GetCurrentMonthOrders(empID);
    [HttpGet]
    [Route("GetCancelled")]
    public IList<OrderModel> GetCancelledorders(string empID) => _orderData.GetCancelledOrders(empID);
    //----------End Filtering methods

    // GET: api/<ordersController>/5
    [HttpGet]
    [Route("GetNeworders")]
    public IList<OrderModel> GetNeworders(int serviceId)
    {
        try
        {
            if (serviceId > 0)
            {
                IList<OrderModel> newOrders = _orderData.GetNewOrders(serviceId);
                return newOrders;
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    // GET api/<ordersController>/string
    [HttpGet]
    [Route("GetProviderorders")]
    public IList<OrderModel> GetProviderorders(string providerId)
    {
        try
        {
            IList<OrderModel> orders = _orderData.GetOrders(providerId);
            return orders;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    // GET api/<ordersController>/string
    [HttpGet]
    [Route("GetTask")]
    public TaskModel GetTask(int Id)
    {
        try
        {
            if (Id > 0)
            {
                TaskModel order = _orderData.GetTask(Id);
                return order;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    // POST api/<ordersController>
    [HttpPost]
    [Route("Post")]
    public void Post(OrderModel orderModel)
    {
        try
        {
            _orderData.InsertOrder(orderModel);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    // PUT api/<ordersController>/5
    [HttpPut]
    [Route("Update")]
    public void Update(TaskModel taskUpdate)
    {
        try
        {
            _orderData.UpdateTask(taskUpdate);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

}
