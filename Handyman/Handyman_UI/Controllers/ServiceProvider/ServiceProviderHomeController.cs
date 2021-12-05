using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers.ServiceProvider
{
    public class ServiceProviderHomeController : Controller
    {
        //interfaces for classes
        private IServiceProviderEndPoint _serviceProvider;
        private IServicesLoader _serviceLoader;
        private IAPIHelper _apiHepler;
        private IProfileEndPoint _profileEndPoint;

        static private string Username, Password;
        public static string sessionToken;
        private ServiceProviderModel serviceProvider;
        private ProfileModel profile;


        public ServiceProviderHomeController(IAPIHelper aPIHelper, IServiceProviderEndPoint registerProvider, IProfileEndPoint profileEndPoint,IServicesLoader servicesLoader)
        {
            _apiHepler = aPIHelper;
            _serviceProvider = registerProvider;
            _profileEndPoint = profileEndPoint;
            _serviceLoader = servicesLoader;
        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Requests()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult ServiceDisplay()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult ActiveRequests()
        {
            return View();
        }
        public ActionResult Test()
        {



            return View();
        }

        //[HttpPost]
        public async Task<ActionResult> RegisterServiceProvider(ServiceProviderDisplayModel serviceProviderDisplay)
        {

            /********Provider Type Dropdown list*/
            profile = new ProfileModel();
            profile.Address = new ProfileModel.AddressModel();
            serviceProvider = new ServiceProviderModel();
            var tempProvider = new ServiceProviderDisplayModel();
            //the cheat
            tempProvider.CompanyName = "Name";
            tempProvider.ServiceProviderTypesId = 0;
            tempProvider.SelectedServiceId = 0;
            tempProvider.providerServices = new List<ServiceDisplayModel>();
           

            

                List<SelectListItem> serviceProviderTypeslist = new List<SelectListItem>()
                    {
                      new SelectListItem {Text = "Individual", Value = "1"},
                      new SelectListItem {Text = "Small Business", Value = "2"},
                      new SelectListItem {Text = "Organzation", Value = "3"},
                      new SelectListItem {Text = "Enterprise/Private Group", Value = "4"}

                    };

                tempProvider.serviceProviderTypes = serviceProviderTypeslist;
                serviceProviderDisplay.serviceProviderTypes = serviceProviderTypeslist;

                var selectedItem = serviceProviderTypeslist.Find(p => p.Value == serviceProviderDisplay.ServiceProviderTypesId.ToString());

                if (selectedItem != null)
                {
                    selectedItem.Selected = true;
                    serviceProvider.ProviderType = selectedItem.Text;
                }
                /*END PROVIDER TYPE************************************/



                /**********************************Services DropdownList*/

                List<SelectListItem> services = new List<SelectListItem>();


                var providersServiceDisplayModel = new ProvidersServiceDisplayModel();
                var providersService = new ProvidersServiceModel();
            try
            {
                    List<ServiceDisplayModel> dbServices = await _serviceLoader.getDisplayServices();
                    List<SelectListItem> servicesSelectList = new List<SelectListItem>();
                    for (int i = 0; i < dbServices.Count; i++)
                    {
                        //Populating services from db into a dropdownlist
                        servicesSelectList.Add(new SelectListItem { Text = dbServices.ElementAt(i).Name, Value = $"{dbServices.ElementAt(i).Id}" });
                    }

                    tempProvider.ServicesSelectList = servicesSelectList;
                   
                    
                    var selectedProviderItem = servicesSelectList.Find(p => p.Value == serviceProviderDisplay.SelectedServiceId.ToString());

                    if (selectedProviderItem != null)
                    {
                        selectedProviderItem.Selected = true;
                        providersService.ServiceId = Int32.Parse(selectedProviderItem.Value);//convert string into int

                    }

                    /*END SERVICES DROPDOWN LIST********************************/
                    if (ModelState.IsValid)
                    {
                            var _token = Session["Token"].ToString();

                            var loggeduser = await _apiHepler.GetLoggedInUserInfor(_token);
                            var user = new UserModel();
                            user.Id = loggeduser.Id;
                            //Getting a profile with its Address
                            var results = await _profileEndPoint.GetProfile(user);

                            serviceProvider.CompanyName = serviceProviderDisplay.CompanyName;
                            serviceProvider.ProfileId = results.Id;


                            var response = await _serviceProvider.PostServiceProvider(serviceProvider);
                            ViewBag.MessageService = selectedItem.Text;

                            //store the provider's service
                            var providerResult = await _serviceProvider.GetProviderByProfileId(results.Id);
                            providersService.ServiceProviderId = providerResult.Id;
                            var result = await _serviceProvider.PostProvidersService(providersService);

                   
                    }
                

            return View(tempProvider);
        }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
           //The details of the service provider method
        public async Task<ActionResult> ProviderDetails()
        {

            var providerModel = new ServiceProviderDisplayModel();
            if (ModelState.IsValid)
            {
                try
                {


                    var _token = Session["Token"].ToString();

                    var loggeduser = await _apiHepler.GetLoggedInUserInfor(_token);
                    var user = new UserModel();
                    user.Id = loggeduser.Id;
                    //Getting a profile with its Address
                    var results = await _profileEndPoint.GetProfile(user);
                    var response = await _serviceProvider.GetProviderByProfileId(results.Id);
                    var dbProviderServices = await _serviceProvider.GetProvidersServiceByProviderId(response.Id);
                    var dbServices = await _serviceLoader.getDisplayServices();
                    providerModel.providerServices = new List<ServiceDisplayModel>();
                    foreach (ServiceDisplayModel service in dbServices)
                    {
                        foreach (var pservices in dbProviderServices)
                        {


                            if (service.Id == pservices.ServiceId)
                            {
                                providerModel.providerServices.Add(new ServiceDisplayModel { Id = service.Id, ImageUrl = service.ImageUrl, Name = service.Name, ServiceDescription = service.ServiceDescription, Category = service.Category });
                            }
                        }
                    }
                    providerModel.CompanyName = response.CompanyName;
                    providerModel.ProviderType = response.ProviderType;
                    providerModel.services = dbServices;
                    List<SelectListItem> serviceSelectList = new List<SelectListItem>();
                    for (int i = 0; i < dbServices.Count; i++)
                    {
                        //Populating services from db into a dropdownlist
                        serviceSelectList.Add(new SelectListItem { Text = dbServices.ElementAt(i).Name, Value = $"{dbServices.ElementAt(i).Id}" });
                    }
                
                    providerModel.ServicesSelectList = serviceSelectList;

                
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                
            }
            return View(providerModel);
        }

        //Register a new provider's service
        public async Task<ActionResult> AddNewProviderService(int id)
        {
            var _token = Session["Token"].ToString();
            var providerModel = new ServiceProviderDisplayModel();
            var providersService = new ProvidersServiceModel();
            //providerModel.providerServices = new List<ServiceDisplayModel>();
            try
            {
                
                var loggeduser = await _apiHepler.GetLoggedInUserInfor(_token);
                var user = new UserModel();
                user.Id = loggeduser.Id;
                //Getting a profile with its Address
                var results = await _profileEndPoint.GetProfile(user);
                var providerResponse = await _serviceProvider.GetProviderByProfileId(results.Id);
                var dbProviderService = await _serviceProvider.GetProvidersServiceByProviderId(providerResponse.Id);
                var dbServices = await _serviceLoader.getDisplayServices();

               
               
                    if (id!=null)
                    {
                        providersService.ServiceId =id;
                    }
                

                if (ModelState.IsValid)
                {


                    //store the provider's service

                    providersService.ServiceProviderId = providerResponse.Id;

                    var result = await _serviceProvider.PostProvidersService(providersService);
                    RedirectToAction("ProviderDetails", providerModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View();

           
        }
    }
}