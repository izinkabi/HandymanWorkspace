using HandymanUILibrary.Models;
using Handymen_UI_Consumer.Helpers.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Handymen_UI_Consumer.ViewComponents
{
    public class DeleteTaskViewComponent : ViewComponent
    {


        ITasksHelper _taskHelper;

        public DeleteTaskViewComponent(ITasksHelper taskHelper)
        {
            _taskHelper = taskHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            TaskModel task = new()!;
            if (id != 0)
            {
                task = await _taskHelper.GetTask(id);
            }

            return View(task);

        }

    }
}
