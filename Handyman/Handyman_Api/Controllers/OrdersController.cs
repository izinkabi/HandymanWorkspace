using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;


namespace Handyman_Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderData _orderData;
        IEnumerable<OrderTaskModel>? orders;
        public OrdersController(IOrderData orderData)
        {
            _orderData = orderData;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<OrderTaskModel> Get(string consumerId)
        {
            orders = _orderData.GetConsumerOrderAndTasks(consumerId);
            return orders;

        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post([FromBody] OrderModel value)
        {
            _orderData.SaveOrder(value, (List<TaskModel>)value.Tasks);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(OrderModel orderUpdate)
        {
            _orderData.UpdateOrder(orderUpdate);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(string consumerId,int orderId)
        {
            _orderData.DeleteOrderAndTasks(consumerId,orderId);
        }
    }
}
