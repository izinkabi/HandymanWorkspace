using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Handymen_UI_Consumer.Models;
using HandymanUILibrary.API;
using Microsoft.Extensions.Caching.Memory;


namespace Handymen_UI_Consumer.Pages
{

    public class OrderModel : PageModel
    {
        private readonly Handymen_UI_Consumer.Data.Handymen_UI_ConsumerContext _context;
        private readonly IMemoryCache _cache;

        public DateTime CurrentDateTime { get; }

        private Order order;
        private IServiceEndPoint _serviceEndPoint;
        private string ErrorMsg;
        private List<Service> serviceDisplayList;
        public OrderModel(Handymen_UI_Consumer.Data.Handymen_UI_ConsumerContext context,
           IMemoryCache cache, IServiceEndPoint serviceEndPoint)
        {
            _context = context;
            _serviceEndPoint = serviceEndPoint;
            _cache = cache;

        }

        //public Order Order { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public Order OrderProperty
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

        //Referenced by the component
        public List<Service> ServiceDisplayList
        {
            get { return serviceDisplayList; }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            //****Caching ****//
            
            if (!_cache.TryGetValue("order", out Order cacheValue))
            {
                cacheValue = await LoadOrder(id);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(40));

                _cache.Set<Order>("order", cacheValue, cacheEntryOptions);
            }
            order = cacheValue;
            if (order.Id != id)
            {
                return RedirectToPage("./OrderDetails");
            }
            

            //*****End Caching****//

            return Page();
        }

        public async Task OnPostConfirmOrder()
        {

            //****Cache the order****//
            if (!_cache.TryGetValue("order", out Order cacheValue))
            {
                cacheValue = await LoadOrder(order.ServiceId);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(40));

                _cache.Set<Order>("order", cacheValue, cacheEntryOptions);
            }

            order = cacheValue;

            order.IsConfirmed = true;
            ViewData["order"] = order;

        }

        private async Task<Order> LoadOrder(int? id)
        {

            await LoadServices();

            foreach (var service in serviceDisplayList)
            {
                if (service.Id == id)
                {
                    
                    order = new Order();
                    order.Id = service.Id;
                    order.Date = DateTime.Now;
                    order.Status = "Active";
                    order.ServiceId = service.Id;
                    order.ServiceName = service.Name;
                    order.ServiceImageUrl = service.ImageUrl;
                    order.Description = service.Description;

                    return order;

                }
            }
            return order;
        }
        private async Task LoadServices()
        {
            serviceDisplayList = new List<Service>();
            List<HandymanUILibrary.Models.ServiceModel> services = new List<HandymanUILibrary.Models.ServiceModel>();

            try
            {
                //await the endpoint
                services = await _serviceEndPoint.GetServices();

                foreach (var s in services)
                {
                    Service service = new Service();
                    //population
                    service.Name = s.Name;
                    service.Description = s.Description;
                    service.Id = s.Id;
                    //...category
                    service.CategoryId = s.CategoryId;
                    service.CategoryName = s.CategoryName;
                    service.CategoryDescription = s.CategoryDescription;
                    service.CategoryType = s.Type;
                    service.ImageUrl = s.ImageUrl;

                    serviceDisplayList.Add(service);
                    ErrorMsg = null;
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

        }

        public void OnPostCancelOrder()
        {
            _cache.Remove("order");
            _cache.Dispose();
              
        }
    }
}