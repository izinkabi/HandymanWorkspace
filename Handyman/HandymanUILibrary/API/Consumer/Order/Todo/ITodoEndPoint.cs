using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Todo
{
    public interface ITodoEndPoint
    {
        Task PostTodo(List<TodoModel> todoList);
        Task<List<TodoModel>> GetTodoListByOrderId(int Id);
        Task UpdateTodo(TodoModel todoUpdate);
    }
}