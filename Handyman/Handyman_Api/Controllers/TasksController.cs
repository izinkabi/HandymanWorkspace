using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;



namespace Handyman_Api.Controllers;

[Route("api/Tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    readonly ITaskData? _taskData;

    public TasksController(ITaskData? taskData)
    {
        _taskData = taskData;
    }

    // GET: api/Tasks
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/Tasks/5
    [HttpGet]
    [Route("GetTask")]
    public TaskModel Get(int id)
    {
        try
        {
            var t = _taskData.GetTask(id);
            if (t != null) return t;
            return new TaskModel()!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    // POST api/<TasksController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<TasksController>/5
    [HttpPut]
    [Route("Update")]
    public void Put(TaskModel task)
    {
        try
        {
            _taskData.UpdateTask(task);
        }
        catch (Exception)
        {

            throw;
        }
    }

    // DELETE api/<TasksController>/5
    [HttpDelete]
    public void Delete(int id)
    {
        try
        {
            _taskData.DeleteTask(id);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
