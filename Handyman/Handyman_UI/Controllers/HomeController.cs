using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Handyman_UI.Controllers
{
    public class HomeController : Controller
    {

        private IAPIHelper _apiHepler;
        private IloggedInUserModel _loggedInUserModel;
        private IRegisterEndPoint _registerEndPoint;


        static private string DisplayUserName;
        static private string Username;
        static private string Password;

        private IProfileEndPoint _profileEndPoint;

        //private IRegisterProviderEndPoint _registerProviderEndpoint;
       
        private bool loggedIn;
        

        //The use of constructor allows for the Imterfaces to be rendered
        public HomeController(IAPIHelper aPIHelper,IProfileEndPoint profileEndpoint,IloggedInUserModel loggedInUser,IRegisterEndPoint register)
        {
            _apiHepler = aPIHelper;
            _profileEndPoint = profileEndpoint;
            _loggedInUserModel = loggedInUser;
            _registerEndPoint = register;
        }

        public async Task<ActionResult> RegisterServiceProvider(Models.ServiceProviderModel serviceProviderModel)
        {
            
            HandymanUILibrary.Models.ServiceProviderModel sp = new HandymanUILibrary.Models.ServiceProviderModel();
            HandymanUILibrary.Models.ProvidersServiceModel  ps = new HandymanUILibrary.Models.ProvidersServiceModel();
            string singleservice = "";
           

           
            

            
            RegisterProviderEndPoint registerProvider = new RegisterProviderEndPoint(_apiHepler);

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
                sp.UserId = _loggedInUserModel.Id;

                HandymanUILibrary.Models.ServiceProviderModel sprov = new HandymanUILibrary.Models.ServiceProviderModel();
                try
                {
                   

                    await registerProvider.PostServiceProvider(sp);
                    sprov = await registerProvider.GetServiceProviders(sp);

                    ps.ServiceProviderId = sprov.Id;
                    await registerProvider.PostProvidersService(ps);
                    Session["providername"] = _loggedInUserModel.Username;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return RedirectToAction("Index");
            }
           
           

            if (ModelState.IsValid)
            {
                ViewBag.Providername = sp.Name +" " +sp.Surname;


            }

            return View(serviceProviderModel);
            #endregion
        }

        //Home page Action funtion
        public  async Task<ActionResult> Index()
        {
          
            ViewBag.Username = DisplayUserName;
            return View();
        }

        public async Task<ActionResult> ElectronicCategory()
        {
            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "Your application description page.";
            ViewBag.Username = DisplayUserName;
            return View();
        }
        public ActionResult Services()
        {

            ViewBag.Message = "Your application description page.";
            ViewBag.Username = DisplayUserName;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Username = DisplayUserName;
            ViewBag.Message = "Your contact page.";
            return View();
        }
        
        //Login action function
        public async Task<ActionResult> SignIn(UserLoginModel model)
        {

            Username = model.Username;
            Password = model.Password;

            if (ModelState.IsValid)
            {
            
                try
                {
                    var results = await _apiHepler.AuthenticateUser(Username, Password);
                    await _apiHepler.GetLoggedInUserInfor(results.Access_Token);
                    DisplayUserName = results.UserName;
                    Session["log"] = results.Access_Token;
                    Session["profilename"] = DisplayUserName;
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }
               
               
            }
            ViewBag.Username = DisplayUserName;

            return View();
           
        }


        //Create a profile action method
        public async Task<ActionResult> CreateProfile(Models.ProfileModel profile)
        {

           
           


            if (ModelState.IsValid)
            {

                HandymanUILibrary.Models.ProfileModel profileModel = new HandymanUILibrary.Models.ProfileModel();
                profileModel.Name = profile.Name;
                profileModel.Surname = profile.Surname;
                profileModel.PhoneNumber = profile.PhoneNumber;
                profileModel.HomeAddress = profile.HomeAddress;
                profileModel.DateofBirth = profile.DateTimeOfBirth;
                profileModel.Type = "user";
                ViewBag.profilename = profile.Name + profile.Surname;

                try
                    {
                        //We await multiple tasks to create the profile
                        //First Get the token with auth and get the loggedin user
                        //Then pass the user ID but keep it to track the user 
                        //Finally we await the profile post task and if successful redict to the mentioned view/method


                        var results = await _apiHepler.AuthenticateUser(Username, Password);//auth awaited task
                            
                        await _apiHepler.GetLoggedInUserInfor(results.Access_Token);// awaited loggedUser

                        profileModel.UserId = _loggedInUserModel.Id;//pass user ID
                       
                        await _profileEndPoint.PostProfile(profileModel);//waited results of posted user profile
                        Session["profilename"] = _loggedInUserModel.Token;
                        return RedirectToAction("Index");

                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMsg = ex.Message;
                    }
                
                //else
                //{
                //     await _apiHepler.GetLoggedInUserInfor(Token);
                //    profileModel.UserId = loggedInUserModel.Id;

                //    await _profileEndPoint.PostProfile(profileModel);
                //    return RedirectToAction("Index");
                //}
                

            }

            return View();
        }


        //RegisterUser helper method
        //saving a user in our local DB

        public async Task<ActionResult> Register(CreateUserModel newUser)
        {
           
            if (ModelState.IsValid)
            {
                try {
                    

                    HandymanUILibrary.Models.NewUserModel user = new HandymanUILibrary.Models.NewUserModel();

                    //Username and password are given a value once here and once in sign in
                    //that might need a credentials interface and its class
                    user.Email = newUser.Username;
                    Username = newUser.Username;

                    //UserEmail = newUser.Username;
                    //Username = UserEmail;
                    Password = newUser.Password;
                    user.Password = newUser.Password;
                    user.ConfirmPassword = newUser.ConfirmPassword;
               
                
                    var result = await _registerEndPoint.RegisterUser(user);
                   
                    var results = await _registerEndPoint.SaveNewUser(user);
                    
                    //UserEmail = Username;
                    //await SaveUser();
                    Session["log"] = result.Email;
                    


                    return RedirectToAction("SignIn");
                    
                }
                catch (Exception ex)
                {
                    ViewBag.RegisterErrorMsg = ex.Message;
                }
                ViewBag.Username = Username;
            }
                return View();
        }


       
        //Get OTP
        public async Task<ActionResult> GetOTP(OTPModel otmodel)
        {
            if (ModelState.IsValid)
            {
                //might not work
                //if (oTPGenerator==null)
                //{

                //    oTPGenerator = new OTPGenerator();
                //    var result = await oTPGenerator.CreateOTPEnvironment(UserId, PhoneNumber, Username);
                    
                //    try
                //    {
                //        //await the OTP to validate
                //        var lastValidation = await oTPGenerator.ValidateOTP(otmodel.OTP);
                //        ViewBag.ErrorMsgCode = lastValidation.messageDescription;
                //        return RedirectToAction("Index");

                //    }
                //        catch (Exception ex)
                //        {
                //            throw new Exception(ex.Message);
                //        }
                //}
                //    else
                //    {
                //        var lastValidation = await oTPGenerator.ValidateOTP(otmodel.OTP);
                //        ViewBag.ErrorMsgCode = lastValidation.messageDescription;
                //        return RedirectToAction("Index");
                //    }

            }

            return View();
        }

        //Loggin out method
        public ActionResult Logout()
        {
            //clear the instances in the container
            Session["providername"] = null;
            Session["profilename"] = null;
            _loggedInUserModel = null;
           
            _apiHepler.LogOutuser();
            Session["log"] = null;
            _apiHepler = null;          
               return RedirectToAction("Index");
          
        }
    }
}