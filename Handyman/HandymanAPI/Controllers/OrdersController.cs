using HandymanDataLibray.DataAccess;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private static OrderData orderData;
      
        // GET: api/Orders/Z
        [Route("api/GetOrdersByConsumerId")]
        public IEnumerable<OrderModel> Get(string Id)
        {
            orderData = new OrderData();
            return orderData.GetOrdersByConsumerId(Id);
        }


        // POST: api/Orders
        //[Route("api/PostOrder")]
        //public void Post(OrderModel order)
        //{
        //    orderData = new OrderData();
        //    orderData.PostOrder(order);
        //}

        //Save the order and its todolist
        [Route("api/SaveOrder")]
        public void Post(OrderModel order)
        {
            orderData = new OrderData();
            orderData.SaveOrder(order, order.TodoItems.ToList());
        }

        // PUT: api/Orders/5
        [Route("api/UpdateOrder")]
        public void Put(OrderModel orderUpdate)
        {
            orderData = new OrderData();
            orderData.PostOrder(orderUpdate);
        }

        // DELETE: api/Orders/5
        [Route("api/DeleteOrder")]
        public void Delete(int id)
        {
            orderData = new OrderData();
            try
            {
            orderData.DeleteOrder(id);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}
