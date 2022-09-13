using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Todo
{
    public interface ITodoEndPoint
    {
        Task<TodoModel> PostTodo(TodoModel todo);
        Task<List<TodoModel>> GetTodoListByOrderId(string Id);
        Task UpdateTodo(TodoModel todoUpdate);
    }
}