using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Handyman_UI.Controllers.ServiceProvider
{
    public sealed class ServiceProviderHomeController : LoginController 
    {
        //interfaces for classes
        private IServiceProviderEndPoint _serviceProviderEndPoint;
        private IServicesLoader _serviceLoader;
       // IServiceProviderEndPoint _serviceProviderEndPoint;




        public static string sessionToken;
        private ServiceProviderModel serviceProvider;
        private ProfileModel profile;
        private List<ServiceDisplayModel> localServices;
        private Helpers.ServiceProviderHelper providersHelper;
        private string ErrorMsg;
        private bool isUpdatedProfile;

        //private bool isUpdatedProfile { get { return _isUpdatedProfile; } set { _isUpdatedProfile = value; } }
        public ServiceProviderHomeController(IAPIHelper aPIHelper, IProfileEndPoint profile, IRegisterEndPoint registerEndPoint
            , IloggedInUserModel loggedInUserModel,IServiceProviderEndPoint serviceProviderEndPoint, 
            IServicesLoader servicesLoader) : base(aPIHelper,profile,registerEndPoint,loggedInUserModel)
        {
           
            _serviceProviderEndPoint = serviceProviderEndPoint;
            _profileEndPoint = profile;
            _serviceLoader = servicesLoader;
            _serviceProviderEndPoint = serviceProviderEndPoint;
            providersHelper = new Helpers.ServiceProviderHelper(serviceProviderEndPoint);
        }


        public ActionResult ServiceProviderLogin()
        {
            Session.Clear();//clear the old session if logged in
            return RedirectToAction("SignIn", "Login");
        }
        public async Task<ActionResult> Home()
        {
            profile = (ProfileModel)Session["loggedinprofile"];

            if (profile!=null)
            {

                
                try
                {
                    serviceProvider = await _serviceProviderEndPoint.GetProviderByProfileId(profile.Id);//we can store this in a session so that when a provider logs in it starts a session
                    ServiceProviderDisplayModel serviceProviderModel = new ServiceProviderDisplayModel();
                    serviceProviderModel.services = new List<ServiceDisplayModel>();
                    if (serviceProvider != null)
                    {
                        providersHelper.IsServiceProvider = true;//it is a service provider

                        serviceProviderModel.services = await _serviceLoader.getDisplayServices();
                        
                    }
                    return PartialView(serviceProviderModel);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return RedirectToAction("SignIn", "Login");
            }
          
            //return View(serviceProvider);
        }
        
        //public ActionResult Requests()
        //{
        //    return View();
        //}
        //public ActionResult Services()
        //{
        //    return View();
        //}

        //public ActionResult AddressDetails()
        //{
        //    return PartialView();
        //}
        //public ActionResult ServiceDisplay()
        //{
        //    return View();
        //}
        //public ActionResult Profile()
        //{
        //    return View();
        //}
        //public ActionResult ActiveRequests()
        //{
        //    return View();
        //}
        public async Task<ActionResult> Test()
        {
            localServices = new List<ServiceDisplayModel>();
            localServices = await _serviceLoader.getDisplayServices();
            return View(localServices);
        }

        public async Task<ActionResult> Search(string serviceSearchString)
        {
            localServices = new List<ServiceDisplayModel>();
            try
            {

                var dbeServices = await _serviceLoader.getDisplayServices();

           
            var tempServices = new List<ServiceDisplayModel>();
            foreach (var service in dbeServices)
            {
                if ((service.Name.Contains(serviceSearchString)) || (service.Category.Contains(serviceSearchString)))
                {
                    localServices.Add(service);

                }
            }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View(localServices);

        }

        //[HttpPost]
        public async Task<ActionResult> RegisterServiceProvider(ServiceProviderDisplayModel serviceProviderDisplay)
        {

            /********Provider Type Dropdown list*/
            var profile = new ProfileModel();
            profile.Address = new ProfileModel.AddressModel();
            serviceProvider = new ServiceProviderModel();
            var tempProvider = new ServiceProviderDisplayModel();
            //the cheat
            tempProvider.CompanyName = "";
            
            tempProvider.providerServices = new List<ServiceDisplayModel>();
            tempProvider.Profile = new ProfileModel();
            tempProvider.Profile.Address = new ProfileModel.AddressModel();
           

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

                            //_logge = await _apiHepler.GetLoggedInUserInfor(_token);
                            //var user = new UserModel();
                            //user.Id = loggeduser.Id;
                            //Getting a profile with its Address
                            //var results = await _profileEndPoint.GetProfile(user);

                            //serviceProvider.CompanyName = serviceProviderDisplay.CompanyName;
                            //serviceProvider.ProfileId = results.Id;


                            var response = await _serviceProviderEndPoint.PostServiceProvider(serviceProvider);
                            ViewBag.MessageService = selectedItem.Text;

                            //store the provider's service
                            var providerResult = await _serviceProviderEndPoint.GetProviderByProfileId(0);/*****/
                            providersService.ServiceProviderId = providerResult.Id;
                            var result = await _serviceProviderEndPoint.PostProvidersService(providersService);
                            TempData["newuser"] = "A new Service Provider, Handyman";
                            
                    return RedirectToAction("Home", "ServiceProviderHome");
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
            providerModel.Profile = new ProfileModel();
            providerModel.Profile.Address = new ProfileModel.AddressModel();
            if (ModelState.IsValid)
            {
                try
                {

                    //_loggedInUserModel = (IloggedInUserModel)Session["loggedinuser"];
                     //profile = (ProfileModel)Session["loggedinprofile"];
                    if (profile==null || isUpdatedProfile)
                    {
                        profile = (ProfileModel)Session["loggedinprofile"];//update
                        Session["loggedinprofile"] = profile;
                    }
                    

                    //get the provider and its services
                    if (profile != null)
                    {

                        serviceProvider = await _serviceProviderEndPoint.GetProviderByProfileId(profile.Id);
                        var dbProviderServices = await _serviceProviderEndPoint.GetProvidersServiceByProviderId(serviceProvider.Id);
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
                        providerModel.CompanyName = serviceProvider.CompanyName;
                        providerModel.ProviderType = serviceProvider.ProviderType;
                        providerModel.Profile = profile;
                        providerModel.services = dbServices;
                        List<SelectListItem> serviceSelectList = new List<SelectListItem>();
                        for (int i = 0; i < dbServices.Count; i++)
                        {
                            //Populating services from db into a dropdownlist
                            serviceSelectList.Add(new SelectListItem { Text = dbServices.ElementAt(i).Name, Value = $"{dbServices.ElementAt(i).Id}" });
                        }

                        providerModel.ServicesSelectList = serviceSelectList;

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                
            }
            return View(providerModel);
        }

        //The two following methods delete the provider's service
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteService(int? id)
        {
            if (id != null)
            {

                var service = await _serviceLoader.getServiceById(id.Value);
                TempData["servicedeleted"] = "You deleted " + service.Name;
                return View(service);
            }
            return View();
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {


            //var token = Session["Token"].ToString();

            //UserModel user = new UserModel();
            //var loggedUseResult = await _apiHepler.GetLoggedInUserInfor(token);
            //user.Id = loggedUseResult.Id;

            //var results = await _profileEndPoint.GetProfile("");
            //var response = await _serviceProviderEndPoint.GetProviderByProfileId(results.Id);
            if (profile == null)
            {
                
                profile = (ProfileModel)Session["loggedinprofile"];
            }
            serviceProvider = await _serviceProviderEndPoint.GetProviderByProfileId(profile.Id);
            var dbProviderServices = await _serviceProviderEndPoint.GetProvidersServiceByProviderId(serviceProvider.Id);


            if (dbProviderServices == null)
            {
                return HttpNotFound();
            }

            //get the service id of the provider's service from the list of provider's services and delete it
            foreach (var provservice in dbProviderServices)
            {
                if (provservice.ServiceId == id)
                {                  
                    await _serviceProviderEndPoint.DeleteProvidersService(provservice.Id);
                    var service = await _serviceLoader.getServiceById(provservice.ServiceId);
                    TempData["servicedeleted"] = "You deleted " + service.Name;

                }
            }



           
            return RedirectToAction("ProviderDetails");
           
        }
        public async Task<ActionResult> Edit(int? id)
        {
            var providerModel = new ServiceProviderDisplayModel();
            if (ModelState.IsValid)
            {
                try
                {
                    //Getting a profile with its Address

                    if(profile == null)
                    {
                        profile = (ProfileModel)Session["loggedinprofile"];

                    }
                    
                    

                    var providerResponse = await _serviceProviderEndPoint.GetProviderByProfileId(profile.Id);
                    List<ProvidersServiceModel> providerServices  = await _serviceProviderEndPoint.GetProvidersServiceByProviderId(providerResponse.Id);
                    List<ServiceDisplayModel> services = await _serviceLoader.getDisplayServices();
                    providerModel.providerServices = new List<ServiceDisplayModel>();
                    foreach (ProvidersServiceModel psm in providerServices)
                    {
                        for(int i = 0; i < services.Count; i++)
                        {
                            if (psm.ServiceId == services[i].Id)
                            {
                                providerModel.providerServices.Add(services[i]);
                            }
                        }
                    }
                    List<SelectListItem> serviceProviderTypeslist = new List<SelectListItem>()
                    {
                      new SelectListItem {Text = "Individual", Value = "1"},
                      new SelectListItem {Text = "Small Business", Value = "2"},
                      new SelectListItem {Text = "Organzation", Value = "3"},
                      new SelectListItem {Text = "Enterprise/Private Group", Value = "4"}

                    };
                    providerModel.serviceProviderTypes = 
                        serviceProviderTypeslist;
                    if (profile.Address!=null && profile.Address is ProfileModel.AddressModel)
                    {
                        providerModel.Profile = profile;
                        providerModel.ProviderType = providerResponse.ProviderType;
                        providerModel.CompanyName = providerResponse.CompanyName;
                        providerModel.ProviderType = providerResponse.ProviderType;
                        providerModel.Id = providerResponse.Id;
                    }
                        

                    return View("Edit", providerModel);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ServiceProviderDisplayModel providerDisplay)
        {
            var providerUpdate = new ServiceProviderModel();
           
          
            //var profileUpdate = new ProfileModel();
           

            if (ModelState.IsValid)
            {
                try
                {

                    //providerDisplay.serviceProviderTypes = serviceProviderTypeslist;



                    List<SelectListItem> serviceProviderTypeslist = new List<SelectListItem>()
                    {
                      new SelectListItem {Text = "Individual", Value = "1"},
                      new SelectListItem {Text = "Small Business", Value = "2"},
                      new SelectListItem {Text = "Organzation", Value = "3"},
                      new SelectListItem {Text = "Enterprise/Private Group", Value = "4"}

                    };
                    providerDisplay.serviceProviderTypes = serviceProviderTypeslist;
                    var selectedItem = serviceProviderTypeslist.Find(p => p.Value == providerDisplay.ServiceProviderTypesId.ToString());

                    if (selectedItem != null)
                    {
                        selectedItem.Selected = true;
                        providerDisplay.ProviderType = selectedItem.Text;
                    }

                    if (providerDisplay.Profile !=null && providerDisplay is ServiceProviderDisplayModel)
                    {
                       
                        providerUpdate.ProfileId = providerDisplay.Profile.Id;
                        providerUpdate.ProviderType = providerDisplay.ProviderType;
                        providerUpdate.CompanyName = providerDisplay.CompanyName;
                        providerUpdate.Id = providerDisplay.Id;

                        await _profileEndPoint.UpdateProfile(providerDisplay.Profile);
                        await _serviceProviderEndPoint.UpdateServiceProvider(providerUpdate);
                        TempData["updatedprofile"] = providerDisplay.Profile.Name + " you edited your Profile";
                        isUpdatedProfile = true;
                        Session["loggedinprofile"] = providerDisplay.Profile;//update the profile in the session
                       

                    }

                }
                catch (Exception ex)
                {
                    ErrorMsg = (ex.Message);
                    return RedirectToAction("ServiceProviderLogin");
                }
               
                return RedirectToAction("ProviderDetails");
            }
            return View();
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

                //var loggeduser = await _apiHepler.GetLoggedInUserInfor(_token);
                //var user = new UserModel();
                //user.Id = loggeduser.Id;
                //Getting a profile with its Address
                //var results = await _profileEndPoint.GetProfile("");
                profile = (ProfileModel)Session["loggedprofile"];
                if (profile != null)
                {
                    var providerResponse = await _serviceProviderEndPoint.GetProviderByProfileId(profile.Id);
                    var dbProviderService = await _serviceProviderEndPoint.GetProvidersServiceByProviderId(providerResponse.Id);




                    if (ModelState.IsValid)
                    {


                        //store the provider's service

                        providersService.ServiceProviderId = providerResponse.Id;

                        var result = await _serviceProviderEndPoint.PostProvidersService(providersService);
                        var service = await _serviceLoader.getServiceById(providersService.ServiceId);
                        TempData["serviceadded"] = "You Added " + service.Name;
                        return RedirectToAction("ProviderDetails");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = (ex.Message);
            }

            return View();

           
        }
    }
}