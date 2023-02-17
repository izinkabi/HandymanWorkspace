using Handymen_UI_Consumer.Areas.Identity.Data;
using Handymen_UI_Consumer.Helpers;
using Handymen_UI_Consumer.Helpers.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages;

public class OrderDetailsModel : PageModel
{
    SignInManager<Handymen_UI_ConsumerUser> _signInManager;
    HandymanUILibrary.Models.OrderModel order;
    IOrderHelper? _orderHelper;
    ITasksHelper? _taskHelper;


    string? ErrorMsg;
    bool isConfirmed;
    bool isBilled;
    bool isClosed;
    bool isCanceled;


    public OrderDetailsModel(IOrderHelper orderHelper,
        SignInManager<Handymen_UI_ConsumerUser> signInManager, ITasksHelper tasksHelper)
    {
        _orderHelper = orderHelper;
        _signInManager = signInManager;
        _taskHelper = tasksHelper;
    }

    public bool IsConfirmed { get { return isConfirmed; } set { isConfirmed = value; } }

    //The OrderModel as a class property
    [BindProperty(SupportsGet = true)]
    public HandymanUILibrary.Models.OrderModel OrderProperty
    {
        get { return order; }

        private set
        {
            order = value;
            if (order.status == 4)
            {
                isConfirmed = true;
            }
            if (order.status == 5)
            {
                isConfirmed = true;
                isBilled = true;
            }
            if (order.status == 6)
            {
                isConfirmed = true;
                isBilled = true;
                isCanceled = true;
            }
        }
    }


    //Get on Page redirection and 
    public async Task<IActionResult> OnGetAsync(int? id)
    {

        //order.service = new()!;

        if (id == null)
        {
            return NotFound();
        }

        try
        {
            OrderProperty = await _orderHelper.GetOrderById(id.Value, _signInManager.UserManager.GetUserId(User));

        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;
        }

        return Page();
    }

    //Deleting an order
    public async Task<RedirectResult> OnPostCancelOrderAsync(int? id)
    {
        try
        {
            if (id != null)
                if (User != null)
                {
                    var cancellationOrderModel = await _orderHelper.GetOrderById(id.Value, _signInManager.UserManager.GetUserId(User));
                    if (cancellationOrderModel != null)
                    {
                        if (cancellationOrderModel.ConsumerID == null)
                        {
                            cancellationOrderModel.ConsumerID = _signInManager.UserManager.GetUserId(User);
                        }

                        if (cancellationOrderModel.ConsumerID != null)
                        {
                            _orderHelper.CancelOrder(cancellationOrderModel);
                        }
                    }
                }


        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;
        }
        return Redirect("./Index");

    }

    /// <summary>
    /// Confirm the order is finished, so this task checks a order's status and upgrade it
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IActionResult> OnPostConfirm(int id)
    {
        try
        {
            if (id != null)
            {
                //This is a result of a null reference exception at runtime on  a order-variable,
                //nor was passing a model helping. But an integer id as a parameter worked.
                var orderModel = await _orderHelper.GetOrderById(id, _signInManager.UserManager.GetUserId(User));

                if (orderModel != null && orderModel.service != null)
                {
                    if (orderModel.status == 4) { return Page(); }

                    orderModel.status = 4;
                    orderModel.ConsumerID = _signInManager.UserManager.GetUserId(User);
                    await _orderHelper.UpdateOrderStatus(orderModel);
                    order = new()!;
                    order = orderModel;
                    isConfirmed = true;
                }
            }

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
        return Page();
    }

    //Cancel a task in an order
    public async Task<RedirectResult> OnPostCancelTaskAsync(int? Id)
    {
        try
        {
            if (Id != 0)
            {
                var cancelTask = await _taskHelper.GetTask(Id.Value);
                _taskHelper.CancelTask(cancelTask);
            }
        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;
        }

        return Redirect("./Index");
    }
}
