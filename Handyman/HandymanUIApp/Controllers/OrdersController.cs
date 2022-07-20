using Microsoft.AspNetCore.Mvc;
using HandymanUILibrary.API.Consumer;
using HandymanUIApp.Models;
using Microsoft.AspNetCore.Identity;

namespace HandymanUIApp.Controllers
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private IOrderEndPoint _orderEndPoint;
        private List<OrderModel>?  AllOrders;
        private OrderModel? _order;
        private UserManager<IdentityUser> _UserManager;

        //Construction of Orders controller and Injecting the dependancies
        public OrdersController(UserManager<IdentityUser> UserManager, IOrderEndPoint orderEndPoint)
        {
            _UserManager = UserManager;
            _orderEndPoint =   orderEndPoint;
            
        }

        //Main view of the Orders that displays a list of Orders
        public async Task<IActionResult> OrdersHome()
        {

            if(AllOrders==null)
            {
                try
                {
                    //httpContextAccessor gets the loggedInUserId
                    //AllOrders = await LoadOrders(_UserManager.GetUserId(User));//LoadOrders(_httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                }catch(Exception ex)
                {
                    return RedirectToAction("Error",ex.Message);
                }
               
            }
           return View(AllOrders);
        }

        //a method for displaying a loading component       
        [Route("ConfirmOrder"),ActionName("ConfirmOrder")]
        public IActionResult ConfirmOrder()
        {
            //ViewBag["LoaingActivator"] = "Loading Providers";
            return View();
        }

        //Initiation of order cancellation
        [Route("CancelOrder")]
        public IActionResult CancelOrder()
        {
            if (_order != null) 
            {
                _order = null;

            }
            return RedirectToAction(actionName:"ServiceHome", controllerName: "Services");
        }
        //Get/Order
        [HttpGet,ActionName("CreateOrder")]
        public IActionResult CreateOrder(ServiceModel service)
        {
             var _order = new OrderModel();
                //var tempService = new ServiceModel();
                if (service != null)
                {
                    //tempService = TempData["NewOrder"] as ServiceModel;
                    _order.ServiceName = service.Name;
                    _order.ServiceId = service.Id;
                    _order.Description = service.Description;
                    _order.ServiceImageUrl = service.ImageUrl;
                    _order.Date = DateTime.Now;//this is bound to change invirtue of the provider assignment time
                    //status is set after assigning a provider to a request irrelevent here
                       
                    //A call to SignalR End point and await a response of a provider found or not
                    //If found return the Order with provider details included
                    
                    return View(_order);
                }
            
            return View();
        }
       
        //Order Details display after a request is accepted by provider
        public IActionResult Details()
        {

            return View(_order);
        }

        //This will load All the orders for the customer
        //private async Task<List<OrderModel>> LoadOrders(string userId)
        //{
        ////    //populate the order list 
        ////    List<HandymanUILibrary.Models.OrderModel> ordersData = new List<HandymanUILibrary.Models.OrderModel>();
        ////    AllOrders = new List<OrderModel>();
        ////    ordersData = await _orderEndPoint.GetOrders(userId);
        ////    foreach(var order in AllOrders)
        ////    {
        ////        var orderModel = new  OrderModel();
        ////        orderModel.Id =  order.Id;
        ////        orderModel.Description = orderModel.Description;
        ////        orderModel.ServiceName = orderModel.ServiceName;
        ////    }
        ////    return AllOrders;
        ////}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}