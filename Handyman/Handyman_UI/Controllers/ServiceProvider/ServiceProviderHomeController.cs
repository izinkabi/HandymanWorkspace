using HandymanUILibrary.API;
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
        private IRegisterProviderEndPoint _registerProvider;
        private IAPIHelper _apiHepler;
        static private string Username, Password;

        public ServiceProviderHomeController(IAPIHelper aPIHelper,IRegisterProviderEndPoint registerProvider)
        {
            _apiHepler = aPIHelper;
            _registerProvider = registerProvider;
        }
        public ActionResult Home()
        {
            return View();
        }

        public async Task<ActionResult> RegisterServiceProvider(Models.ServiceProviderModel serviceProviderModel)
        {

            HandymanUILibrary.Models.ServiceProviderModel sp = new HandymanUILibrary.Models.ServiceProviderModel();
            HandymanUILibrary.Models.ProvidersServiceModel ps = new HandymanUILibrary.Models.ProvidersServiceModel();
            string singleservice = "";

            #region ViewData
            // Services = new List<SelectListItem>();


            List<SelectListItem> Services = new List<SelectListItem>() {

                        new SelectListItem {
                            Text = "Electronic", Value = "1"
                        },
                        new SelectListItem {
                            Text = "Furniture", Value = "2"
                        },
                        new SelectListItem {
                            Text = "Plumbing", Value = "3"
                        },
                        new SelectListItem {
                            Text = "Interrior Design", Value = "4"
                        },
                        new SelectListItem {
                            Text = "Mechanics", Value = "5"
                        },
                        new SelectListItem {
                            Text = "Furniture", Value = "6"
                        },
                        new SelectListItem {
                            Text = "Garderning", Value = "7"
                        }
                     };

            serviceProviderModel.Services = Services;
            var selectedItem = Services.Find(p => p.Value == serviceProviderModel.ServiceId.ToString());

            if (selectedItem != null)
            {
                sp.Name = serviceProviderModel.Name;
                sp.Surname = serviceProviderModel.Surname;
                sp.HomeAddress = serviceProviderModel.HomeAddress;
                sp.PhoneNumber = serviceProviderModel.PhoneNumber;
                sp.DateOfBirth = serviceProviderModel.DateOfBirth;


                selectedItem.Selected = true;
                ViewBag.MessageService = "Service: " + selectedItem.Text;
                ps.ServiceId = Int32.Parse(selectedItem.Value);

                var results = await _apiHepler.AuthenticateUser(Username, Password);//auth awaited task
                //Token = results.Access_Token;//Token
                 await _apiHepler.GetLoggedInUserInfor(results.Access_Token);// awaited loggedUser
                //UserId = loggedInUserModel.Id;//pass user ID
                //sp.UserId = results.;

                HandymanUILibrary.Models.ServiceProviderModel sprov = new HandymanUILibrary.Models.ServiceProviderModel();
                try
                {


                    await _registerProvider.PostServiceProvider(sp);
                    sprov = await _registerProvider.GetServiceProviders(sp);

                    //ps.ServiceProviderId = sprov.Id;
                    await _registerProvider.PostProvidersService(ps);
                    Session["providername"] = sprov.Id;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return RedirectToAction("Profile","spHome");
            }



            if (ModelState.IsValid)
            {
                ViewBag.Providername = sp.Name + " " + sp.Surname;

            }

            return View(serviceProviderModel);
            #endregion
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
    }
}