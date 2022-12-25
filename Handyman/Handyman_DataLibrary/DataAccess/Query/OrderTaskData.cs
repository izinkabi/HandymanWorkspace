using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;


namespace Handyman_DataLibrary.DataAccess.Query
{
    public class OrderTaskData : IOrderTaskData
    {
        ISQLDataAccess _dataAccess;

        public OrderTaskData(ISQLDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        //Update Task
        public void UpdateTask(TaskModel taskUpdate)
        {
            try
            {
                var result = _dataAccess.SaveData<TaskModel>("Request.spTaskUpdate", taskUpdate, "Handyman_DB");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Insert New Task
        public int InsertTask(TaskModel task)
        {
            int taskId = 0;
            try
            {
                task.dateStarted = DateTime.Now;
                task.dateFinished = DateTime.Now;

                taskId = _dataAccess.LoadData<int, dynamic>("Request.spTaskInsert", new
                {
                    dateStarted = task.dateFinished,
                    title = task.title,
                    duration = task.duration,
                    status = task.status,
                    description = task.description,
                    serviceId = task.serviceId,
                    dateFinished = task.dateFinished,

                }, "Handyman_DB").First();

                return taskId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get orders and tasks by order
        public IList<OrderModel> GetOrderAndTasks(int orderId)
        {
            List<OrderTaskModel> ordertasks = new()!;
            HashSet<OrderModel> orderSet = new()!;//It does not allow duplicates
            try
            {
                ordertasks = _dataAccess.LoadData<OrderTaskModel, dynamic>("Delivery.spOrderLookUpByService",
                         new { orderId = orderId }, "Handyman_DB");

                //Braking down the ordertask entity
                //First get get orders then get 
                foreach (var ordertask in ordertasks)
                {
                    var order = new OrderModel();
                    order.service = new();
                    //populate order

                    order.Id = ordertask.ord_id;
                    order.duedate = ordertask.ord_duedate;
                    order.status = ordertask.ord_status;

                    Service_CategoryModel service = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Request.spServiceLookUpBy_Id",
                         new { serviceId = ordertask.ord_service_id }, "Handyman_DB").First();
                    //populate service of each order
                    order.service.name = service.serv_name;
                    order.service.status = service.serv_status;
                    order.service.datecreated = service.serv_datecreated;
                    order.service.img = service.serv_img;
                    order.service.id = service.serv_id;


                    order.service.category = new ServiceCategoryModel();

                    //populate category
                    order.service.category.name = service.cat_name;
                    order.service.category.description = service.cat_description;
                    order.service.category.type = service.cat_type;

                    //Check if the has been populated already
                    foreach (var o in orderSet)
                    {
                        if (o.Id == order.Id)
                        {
                            orderSet.Remove(o);
                        }
                    }
                    orderSet.Add(order);


                }
                //populate task

                foreach (var order in orderSet)
                {
                    order.Tasks = new List<TaskModel>();
                    foreach (var ordertask in ordertasks)
                    {
                        var task = new TaskModel()!;

                        //populate task
                        task.description = ordertask.tas_description;
                        task.dateFinished = ordertask.tas_date_finished;
                        task.dateStarted = ordertask.tas_date_started;
                        task.title = ordertask.tas_title;
                        task.Id = ordertask.task_id;
                        //task.duration = ordertask.tas_duration;
                        if (order.Id == ordertask.ord_id)
                        {
                            order.Tasks.Add(task);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orderSet.ToList();
        }

    }
}
