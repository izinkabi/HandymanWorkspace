using Caliburn.Micro;

using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private ProfileDisplayModel profile;
        private List<ServiceDisplayModel> localServices;
       


        public ServiceProviderHomeController(IAPIHelper aPIHelper, IServiceProviderEndPoint registerProvider, IProfileEndPoint profileEndPoint,IServicesLoader servicesLoader)
        {
            _apiHepler = aPIHelper;
            _serviceProvider = registerProvider;
            _profileEndPoint = profileEndPoint;
            _serviceLoader = servicesLoader;
        }
        public async Task<ActionResult> Home()
        {

            try
            {
                ServiceProviderDisplayModel serviceProviderModel = new ServiceProviderDisplayModel();
                serviceProviderModel.services = new List<ServiceDisplayModel>();
                serviceProviderModel.services = await _serviceLoader.getDisplayServices();

                return PartialView(serviceProviderModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //return View(serviceProvider);
        }

        public ActionResult Requests()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }

        public ActionResult AddressDetails()
        {
            return PartialView();
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
            tempProvider.Profile = new ProfileDisplayModel();
            tempProvider.Profile.AddressM = new ProfileDisplayModel.AddressModel();
           

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
                            TempData["newuser"] = "A new Handyman";
                    return RedirectToAction("SignIn", "Profile");
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
            providerModel.Profile = new ProfileDisplayModel();
            providerModel.Profile.AddressM = new ProfileDisplayModel.AddressModel();
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
                    providerModel.Profile.ProfileId = results.Id;
                    providerModel.Profile.Name = results.Name;
                    providerModel.Profile.PhoneNumber = results.PhoneNumber;
                    providerModel.Profile.Surname = results.Surname;
                    providerModel.Profile.DateOfBirth = results.DateOfBirth;
                    

                    
                    providerModel.Profile.AddressM.City = results.Address.City;
                    providerModel.Profile.AddressM.HouseNumber = results.Address.HouseNumber;
                    providerModel.Profile.AddressM.Id = results.Address.Id;
                    providerModel.Profile.AddressM.PostalCode = results.Address.PostalCode;
                    providerModel.Profile.AddressM.StreetName = results.Address.StreetName;
                    providerModel.Profile.AddressM.Surburb = results.Address.Surburb;


                    //get the provider and its services
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

        //The two following methods delete the provider's service
        public async Task<ActionResult> Delete(int? id)
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


            var token = Session["Token"].ToString();

            UserModel user = new UserModel();
            var loggedUseResult = await _apiHepler.GetLoggedInUserInfor(token);
            user.Id = loggedUseResult.Id;

            var results = await _profileEndPoint.GetProfile(user);
            var response = await _serviceProvider.GetProviderByProfileId(results.Id);
            var dbProviderServices = await _serviceProvider.GetProvidersServiceByProviderId(response.Id);


            if (dbProviderServices == null)
            {
                return HttpNotFound();
            }

            //get the service id of the provider's service from the list of provider's services and delete it
            foreach (var provservice in dbProviderServices)
            {
                if (provservice.ServiceId == id)
                {                  
                    await _serviceProvider.DeleteProvidersService(provservice.Id);
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
                    providerModel.Profile = new ProfileDisplayModel();

                    var _token = Session["Token"].ToString();

                    var loggeduser = await _apiHepler.GetLoggedInUserInfor(_token);
                    var user = new UserModel();
                    user.Id = loggeduser.Id;
                    //Getting a profile with its Address

                    var results = await _profileEndPoint.GetProfile(user);

                    providerModel.Profile.Name = results.Name;
                    providerModel.Profile.PhoneNumber = results.PhoneNumber;
                    providerModel.Profile.Surname = results.Surname;
                    providerModel.Profile.DateOfBirth = results.DateOfBirth;
                    providerModel.Profile.ProfileId = results.Id;
                    

                    var providerResponse = await _serviceProvider.GetProviderByProfileId(results.Id);
                    List<SelectListItem> serviceProviderTypeslist = new List<SelectListItem>()
                    {
                      new SelectListItem {Text = "Individual", Value = "1"},
                      new SelectListItem {Text = "Small Business", Value = "2"},
                      new SelectListItem {Text = "Organzation", Value = "3"},
                      new SelectListItem {Text = "Enterprise/Private Group", Value = "4"}

                    };

                    providerModel.serviceProviderTypes = serviceProviderTypeslist;


                    //var selectedItem = serviceProviderTypeslist.Find(p => p.Value == providerModel.ServiceProviderTypesId.ToString());

                    //if (selectedItem != null)
                    //{
                    //    selectedItem.Selected = true;
                    //    providerModel.ProviderType = selectedItem.Text;

                    //    providerModel.ProviderType = providerResponse.ProviderType;
                    //    //var dbProviderServices = await _serviceProvider.GetProvidersServiceByProviderId(providerResponse.Id);

                    //}
                    providerModel.Profile.AddressM = new ProfileDisplayModel.AddressModel();
                    providerModel.Profile.AddressM.City = results.Address.City;
                    providerModel.Profile.AddressM.HouseNumber = results.Address.HouseNumber;
                    providerModel.Profile.AddressM.Id = results.Address.Id;
                    providerModel.Profile.AddressM.PostalCode = results.Address.PostalCode;
                    providerModel.Profile.AddressM.StreetName = results.Address.StreetName;
                    providerModel.Profile.AddressM.Surburb = results.Address.Surburb;
                    providerModel.CompanyName = providerResponse.CompanyName;
                    return View("Edit",providerModel);
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
           
          
            var profileUpdate = new ProfileModel();
            List<SelectListItem> serviceProviderTypeslist = new List<SelectListItem>()
                    {
                      new SelectListItem {Text = "Individual", Value = "1"},
                      new SelectListItem {Text = "Small Business", Value = "2"},
                      new SelectListItem {Text = "Organzation", Value = "3"},
                      new SelectListItem {Text = "Enterprise/Private Group", Value = "4"}

                    };
            providerDisplay.serviceProviderTypes = serviceProviderTypeslist;

            if (ModelState.IsValid)
            {
                try
                {
                    profileUpdate.Address = new ProfileModel.AddressModel();
                    profileUpdate.Id = providerDisplay.Profile.ProfileId;
                    profileUpdate.Address.City = providerDisplay.Profile.AddressM.City;
                    profileUpdate.Address.StreetName = providerDisplay.Profile.AddressM.StreetName;
                    profileUpdate.Address.Surburb = providerDisplay.Profile.AddressM.Surburb;
                    profileUpdate.Address.PostalCode = providerDisplay.Profile.AddressM.PostalCode;
                    profileUpdate.Address.HouseNumber = providerDisplay.Profile.AddressM.HouseNumber;
                    profileUpdate.Address.Id = providerDisplay.Profile.AddressM.Id;

                    profileUpdate.Name = providerDisplay.Profile.Name;
                    profileUpdate.PhoneNumber = providerDisplay.Profile.PhoneNumber;
                    profileUpdate.Surname = providerDisplay.Profile.Surname;
                    profileUpdate.DateOfBirth = providerDisplay.Profile.DateOfBirth;
                    providerUpdate.ProfileId = providerDisplay.Profile.ProfileId;
                    //first update the profile
                    await _profileEndPoint.UpdateProfile(profileUpdate);


                    providerUpdate.CompanyName = providerDisplay.CompanyName;
                  

                    //Update the service provider

                   // var providerResponse = await _serviceProvider.GetProviderByProfileId(providerDisplay.Profile.ProfileId);
                   

                    


                    var selectedItem = serviceProviderTypeslist.Find(p => p.Value == providerDisplay.ServiceProviderTypesId.ToString());

                    if (selectedItem != null)
                    {
                        selectedItem.Selected = true;
                        providerUpdate.ProviderType = selectedItem.Text;

                        //providerUpdate.ProviderType = providerResponse.ProviderType;
                        //var dbProviderServices = await _serviceProvider.GetProvidersServiceByProviderId(providerResponse.Id);
                      
                    }

                    await _serviceProvider.UpdateServiceProvider(providerUpdate);


                    return RedirectToAction("ProviderDetails");

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

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
                
                var loggeduser = await _apiHepler.GetLoggedInUserInfor(_token);
                var user = new UserModel();
                user.Id = loggeduser.Id;
                //Getting a profile with its Address
                var results = await _profileEndPoint.GetProfile(user);
                var providerResponse = await _serviceProvider.GetProviderByProfileId(results.Id);
                var dbProviderService = await _serviceProvider.GetProvidersServiceByProviderId(providerResponse.Id);
                

               
               
                    if (id!=null)
                    {
                        providersService.ServiceId =id;
                    }
                

                if (ModelState.IsValid)
                {


                    //store the provider's service

                    providersService.ServiceProviderId = providerResponse.Id;

                    var result = await _serviceProvider.PostProvidersService(providersService);
                    var service = await _serviceLoader.getServiceById(providersService.ServiceId);
                    TempData["serviceadded"] = "You Added " + service.Name;
                    return RedirectToAction("ProviderDetails");
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