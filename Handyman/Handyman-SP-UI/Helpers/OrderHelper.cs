using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Helpers;
/// <summary>
/// This class is responsible for the Orders of a service member
/// </summary>
public class OrderHelper : IDisposable, IOrderHelper
{
    IOrderEndpoint? _orderEndpoint;
    IMemberHelper _memberHelper;

    IList<OrderModel> orders;
    readonly IList<OrderModel> _Orders;
    StatusCheckHelper statusCheckHelper;
    OrderModel newOrder;


    public OrderHelper(IOrderEndpoint OrderEndPoint, IMemberHelper MemberHelper)
    {
        _orderEndpoint = OrderEndPoint;
        _memberHelper = MemberHelper;
        orders = new List<OrderModel>();
        statusCheckHelper = new StatusCheckHelper();
    }

    readonly List<OrderModel> NewOrders = new()!;
    List<OrderModel> StartedOrders = new()!;
    List<OrderModel> AcceptedOrders;
    List<OrderModel> FinishedOrders = new()!;
    List<OrderModel> OwnOrders;
    MemberModel member;
    enum OrderStage
    {
        None = 0,
        Accepted = 1,
        Started = 2
    }
    /// <summary>
    /// Helper method for getting all the orders of the given service
    /// These orders will be turned to Orders when accepted by member(s)
    /// For now they are refered to as new Order(s)
    /// </summary>
    /// <param name="serviceId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IList<OrderModel>> GetNewOrders(int serviceId)
    {

        try
        {
            orders = await _orderEndpoint?.GetNewOrdersByService(serviceId);
            if (orders != null)
            {
                return orders;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Get the new Order from the new Orders list
    /// </summary>
    /// <param name="serviceId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    async Task<OrderModel> IOrderHelper.GetNewOrder(int serviceId, int orderId)
    {
        OrderModel newOrder = new()!;

        try
        {
            if (orders.Count < 1)
            {
                orders = await GetNewOrders(serviceId);
            }

            if (orders != null && orders.Count > 0)
            {
                foreach (var o in orders)
                {
                    if (o.order_id == orderId)
                    {
                        newOrder = o;
                        return newOrder;
                    }

                }
            }
            return newOrder;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Get Task Price
    public async Task<PriceModel> GetTaskPrice(int TaskId) => await _orderEndpoint.GetTaskPrice(TaskId);
    //-----Filter Methods
    //Current Week Orders GET
    async Task<List<OrderModel>> IOrderHelper.GetCurrentWeekOrders()
    {
        try
        {
            if (member is null)
            {
                member = await _memberHelper.GetMember();
            }

            if (member != null && member.member_profileId != null)
            {


                var jobs = await _orderEndpoint.GetCurrentWeekOrders(member.member_profileId);
                if (jobs != null && jobs.Count > 0)
                {
                    foreach (var rq in jobs)
                    {
                        rq.order_status = CheckStatus(rq);
                        rq.order_progress = GetProgress(rq);
                    }
                }

                return jobs;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Check Current Month
    async Task<List<OrderModel>> IOrderHelper.GetCurrentMonthOrders()
    {
        try
        {
            if (member is null)
            {
                member = await _memberHelper.GetMember();
            }
            if (member != null && member.member_profileId != null)
            {
                var jobs = await _orderEndpoint.GetCurrentMonthOrders(member.member_profileId);
                if (jobs != null && jobs.Count > 0)
                {
                    foreach (var rq in jobs)
                    {
                        rq.order_status = CheckStatus(rq);
                        rq.order_status = GetProgress(rq);
                    }
                }


                return jobs;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
    public async Task<IList<OrderModel>> GetCancelledOrders(string empID) => await _orderEndpoint.GetCancelledOrders(empID);


    //------End Filter Methods


    //Confirm if the Order is accepted
    async Task<OrderModel> IOrderHelper.ConfirmAccepted(OrderModel OrderChecked)
    {
        if (OrderChecked != null)
        {
            if (await IsAccepted(OrderChecked))
            {
                return newOrder;
            }
        }
        return null;
    }


    /// <summary>
    /// Check if the order has been accepted
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    async Task<bool> IsAccepted(OrderModel order)
    {
        try
        {
            if (AcceptedOrders == null || AcceptedOrders.Count == 0)
            {
                AcceptedOrders = (List<OrderModel>?)await GetOwnOrders();
            }

            if (AcceptedOrders != null && AcceptedOrders.Count > 0)
            {
                foreach (var item in AcceptedOrders)
                {
                    if (item.order_orderid == order.order_id)
                    {
                        newOrder = item;
                        return true;
                    }
                }
            }
            return false;
        }
        catch (Exception)
        {

            throw;
        }
    }
    /// <summary>
    /// Make / create a new Order
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>

    async Task<bool> IOrderHelper.AcceptOrder(OrderModel newOrder)
    {
        OrderModel nr = new()!;
        //populate a Order without a member ID
        nr.order_orderid = newOrder.order_orderid;
        nr.order_datecreated = DateTime.Now;
        nr.order_status = 1;
        nr.order_progress = 1;

        try
        {
            if (!await IsAccepted(newOrder))
            {
                //save the accepted Order
                return await _orderEndpoint.PostOrder(await _memberHelper.StampNewOrder(nr));

            }

        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message, ex.InnerException);
        }
        return false;
    }
    /// <summary>
    /// Get the member's Orders
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    async Task<List<OrderModel>> GetOwnOrders()
    {
        try
        {
            if (member is null)
            {
                member = await _memberHelper.GetMember();//not sure if this guy will budge!
            }

            if (member != null && member.member_profileId != null)
            {

                if (OwnOrders is null)
                {
                    OwnOrders = await _orderEndpoint.GetOrdersByProvider(member.member_profileId);
                }

                if (OwnOrders != null && OwnOrders.Count > 0)
                {
                    foreach (var Order in OwnOrders)
                    {
                        Order.order_status = statusCheckHelper.CheckStatus(Order);
                        Order.order_progress = GetProgress(Order);
                    }

                }
                return OwnOrders;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }



    //------------------------Updating a Order--------------------------//
    public int GetProgress(OrderModel Order) => statusCheckHelper.calculateProgress(Order);
    public int CheckStatus(OrderModel Order) => statusCheckHelper.CheckStatus(Order);

    //------------------------End Updating a Order--------------------------//



    /// <summary>
    /// Look for the Order of the given ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<OrderModel> GetOwnOrder(int id)
    {
        OrderModel Order = new()!;
        try
        {
            if (OwnOrders is null)
            {
                await GetOwnOrders();
            }
            if (OwnOrders is not null && OwnOrders.Count > 0)
            {
                foreach (var r in OwnOrders)
                {
                    if (r.order_id == id)
                    {
                        Order = r;
                    }
                }
                return Order;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
    /// <summary>
    /// Get the task of the given ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    async Task<TaskModel> IOrderHelper.GetTask(int id)
    {
        TaskModel taskModel;
        try
        {
            taskModel = await _orderEndpoint.GetTask(id);
            return taskModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Update a given Order (Update the tasks in the Order)
    /// </summary>
    /// <param name="OrderUpdate"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    async Task IOrderHelper.UpdateTask(TaskModel taskUpdate)
    {
        try
        {
            if (taskUpdate != null)
            {
                await _orderEndpoint.UpdateTask(taskUpdate);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    void IDisposable.Dispose()
    {

    }


    ///////////////////////////////////////////////Status Class//////////////////
    /// <summary>
    /// //Status class
    /// </summary>
    internal class StatusCheckHelper
    {

        List<TaskModel>? startedTasks;
        List<TaskModel>? inprogressTasks;
        List<TaskModel>? finishedTasks;
        List<OrderModel>? cancelledOrders;

        //Return the Order stage
        internal int CheckStatus(OrderModel Order)
        {

            if (Order != null && Order.tasks != null)
            {
                //initialize attributes
                startedTasks = new()!;
                inprogressTasks = new()!;
                finishedTasks = new()!;
                cancelledOrders = new()!;
                //Cancelled Order
                if (Order.order_status == 11)
                {
                    cancelledOrders.Add(Order);
                }

                //Check for task status
                foreach (var t in Order.tasks)
                {

                    //STATUS STARTED
                    if (t.tas_status == 1)
                    {
                        startedTasks.Add(t);
                    }
                    //STATUS IN PROGRESS
                    if (t.tas_status == 2)
                    {
                        inprogressTasks.Add(t);
                    }
                    //STATUS CLOSED
                    if (t.tas_status == 3)
                    {
                        finishedTasks.Add(t);
                    }

                }

                //Order not started
                if (startedTasks.Count == 0 && inprogressTasks.Count == 0 && finishedTasks.Count == 0)
                {
                    return 1;
                }
                else if (startedTasks.Count == 0 && inprogressTasks.Count == 0 && finishedTasks.Count > 0)//Order closed
                {
                    return 3;
                }
                //else if (cancelledOrders != null)
                //{
                //    return 11;
                //}
                else //Order inprogress
                {
                    return 2;
                }


            }
            return 0;//Error

        }

        /// <summary>
        /// a psudo algorithm for getting progress using number of tasks per list
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        internal int calculateProgress(OrderModel Order)
        {

            if (Order != null && Order.tasks != null && Order.tasks.Count > 0)
            {
                if (startedTasks.Count == Order.tasks.Count)
                {
                    return 10;
                }
                if (startedTasks.Count + inprogressTasks.Count == Order.tasks.Count && startedTasks.Count > 0 && inprogressTasks.Count > 0)
                {
                    return 30;
                }
                if (startedTasks.Count + finishedTasks.Count == Order.tasks.Count && startedTasks.Count > finishedTasks.Count && finishedTasks.Count > 0)
                {
                    return 40;
                }
                if (inprogressTasks.Count + finishedTasks.Count == Order.tasks.Count && inprogressTasks.Count != 0 || inprogressTasks.Count == Order.tasks.Count && finishedTasks.Count != Order.tasks.Count)
                {
                    return 50;
                }
                if (finishedTasks.Count == Order.tasks.Count)
                {
                    return 100;
                }
            }
            return 0;
        }



    }




}

