﻿using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Todo
{
    public class TodoEndPoint : ITodoEndPoint
    {
        private IAPIHelper _apiHelper;
        public TodoEndPoint(IAPIHelper helper)
        {
            _apiHelper = helper;    
        }

        //Post the todo and this should pass a list of TodoModels
        public async Task PostTodo(List<TodoModel> todoList)
        {
            HttpResponseMessage responseMessage = new()!;
            try
            {
                responseMessage = await _apiHelper.ApiClient.PostAsJsonAsync<List<TodoModel>>("/api/PostTodoList", todoList );
                 
            }
            catch(Exception ex)
            {
                throw new Exception(responseMessage.ReasonPhrase);

            }
           
        }

        //Get the todoList by the order Id
        public async Task<List<TodoModel>> GetTodoListByOrderId(int Id)
        {


            var httpResponseMessage = await _apiHelper.ApiClient.GetFromJsonAsync<List<TodoModel>>($"/api/GetTodoListByOrderId?Id={Id}");
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

        public async Task UpdateTodo(TodoModel todoUpdate)
        {
            HttpResponseMessage responseMessage = await _apiHelper.ApiClient.PostAsJsonAsync("api/UpdateTodo", todoUpdate);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadFromJsonAsync<TodoModel>();
            }
            else
            {
                throw new Exception(responseMessage.ReasonPhrase);
            }
        }
    }
}
