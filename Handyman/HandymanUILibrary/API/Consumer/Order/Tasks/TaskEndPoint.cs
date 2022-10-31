using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Tasks;

public class TaskEndPoint : ITaskEndPoint
{
    private IAPIHelper _apiHelper;
    public TaskEndPoint(IAPIHelper helper)
    {
        _apiHelper = helper;    
    }

    //Post the todo and this should pass a list of TodoModels
    public async Task PostTask(List<TaskModel> todoList)
    {
        HttpResponseMessage responseMessage = new()!;
        try
        {
            responseMessage = await _apiHelper.ApiClient.PostAsJsonAsync<List<TaskModel>>("/api/PostTodoList", todoList );
             
        }
        catch(Exception ex)
        {
            throw new Exception(responseMessage.ReasonPhrase);

        }
       
    }

    //Get the todoList by the order Id
    public async Task<List<TaskModel>> GetTaskListByOrderId(int Id)
    {


        var httpResponseMessage = await _apiHelper.ApiClient.GetFromJsonAsync<List<TaskModel>>($"/api/GetTodoListByOrderId?Id={Id}");
        /* 
             if (httpResponseMessage.IsSuccessStatusCode)
             {

                 var result = await httpResponseMessage.Content.ReadFromJsonAsync<ConsumerModel>();
                 return result;
             }
             else
             {
                 throw new Exception(httpResponseMessage.ReasonPhrase);
             }*/
        return httpResponseMessage;
    }

    public async Task UpdateTask(TaskModel todoUpdate)
    {
        HttpResponseMessage responseMessage = await _apiHelper.ApiClient.PostAsJsonAsync("api/UpdateTodo", todoUpdate);

        if (responseMessage.IsSuccessStatusCode)
        {
            var result = await responseMessage.Content.ReadFromJsonAsync<TaskModel>();
        }
        else
        {
            throw new Exception(responseMessage.ReasonPhrase);
        }
    }
}

