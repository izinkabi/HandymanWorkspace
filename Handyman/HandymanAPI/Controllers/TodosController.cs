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
    public class TodosController : ApiController
    {

        private TodoData todoData;

        // GET: api/TodoList/Z
        [Route("api/GetTodoListByOrderId")]
        public IEnumerable<TodoModel> GetTodoListByOrderId(int Id)
        {
            todoData = new TodoData();
            return todoData.GetTodoListByOrderId(Id);
        }

        [Route("api/GetTodoById")]
        public TodoModel GetTodoId(int Id)
        {
            todoData = new TodoData();
            return todoData.GetTodoById(Id);
        }
        // POST: api/TodoList
        [Route("api/PostTodoList")]
        public void PostTodoList(List<TodoModel> todoList)
        {
            todoData = new TodoData();
            todoData.PostTodo(todoList);
        }


        // PUT: api/Todo/5
        [Route("api/UpdateTodo")]
        public void UpdateTodo(TodoModel todoUpdate)
        {
            todoData = new TodoData();
            todoData.UpdateTodo(todoUpdate);
        }

        // DELETE: api/Orders/5
        //[Route("api/DeleteOrder")]
        //public void Delete(int id)
        //{
        //    orderData = new OrderData();
        //    try
        //    {
        //        orderData.DeleteOrder(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}



    }
}
