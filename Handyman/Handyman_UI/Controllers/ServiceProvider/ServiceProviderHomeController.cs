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
        private IRegisterProviderEndPoint _registerProvider;
        private IAPIHelper _apiHepler;
        private IProfileEndPoint _profileEndPoint;

        static private string Username, Password;
        public static string sessionToken;
        private ServiceProviderModel serviceProvider;
        private ProfileModel profile;


        public ServiceProviderHomeController(IAPIHelper aPIHelper,IRegisterProviderEndPoint registerProvider, IProfileEndPoint profileEndPoint)
        {
            _apiHepler = aPIHelper;
            _registerProvider = registerProvider;
            _profileEndPoint = profileEndPoint;
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


        public async Task<ActionResult> RegisterServiceProvider(ServiceProviderDisplayModel serviceProviderDisplay)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    profile = new ProfileModel();
                    profile.Address = new ProfileModel.AddressModel();
                    serviceProvider = new ServiceProviderModel();

                    var _token = Session["Token"].ToString();

                    var loggeduser = await _apiHepler.GetLoggedInUserInfor(_token);
                    var user = new UserModel();
                    user.Id = loggeduser.Id;
                    //Getting a profile with its Address
                    var results = await _profileEndPoint.GetProfile(user);

                    List<SelectListItem> serviceProviderTypeslist = new List<SelectListItem>()
            {
              new SelectListItem {Text = "Individual", Value = "1"},
              new SelectListItem {Text = "Small Business", Value = "2"},
              new SelectListItem {Text = "Organzation", Value = "3"},
              new SelectListItem {Text = "Enterprise/Private Group", Value = "4"}

            };

                    serviceProviderDisplay.serviceProviderTypes = serviceProviderTypeslist;
                    var selectedItem = serviceProviderTypeslist.Find(p => p.Value == serviceProviderDisplay.ServiceProviderTypesId.ToString());

                    if (selectedItem != null)
                    {
                        selectedItem.Selected = true;
                        serviceProvider.ProviderType = selectedItem.Text.ToString();
                    }

                    serviceProvider.ProfileId = results.Id;
                    serviceProvider.CompanyName = serviceProviderDisplay.CompanyName;


                    var response = await _registerProvider.PostServiceProvider(serviceProvider);

                    return RedirectToAction("Home", "ServiceProviderHome");
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            return View();
            

        }
    }
}