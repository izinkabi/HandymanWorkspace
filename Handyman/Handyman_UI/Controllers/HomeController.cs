using Handyman_UI.Models;
using HandymanUILibrary.API;
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

        private APIHelper _apiHepler;
        static private string UserEmail { get; set; }
        static private string UserId;


        static private string Token { get; set; }
        static private string Username;
        static private string Password;
        static private int ProfileId;
        static private string PhoneNumber;
        private ProfileEndPoint profileEndPoint;
        private RegisterProviderEndPoint registerProviderEndpoint;
        private OTPGenerator oTPGenerator;
        private bool loggedIn;
        


        public async Task<ActionResult> RegisterServiceProvider(ServiceProviderModel serviceProviderModel)
        {
            //ServiceProviderModel serviceProvider = new ServiceProviderModel();
            HandymanUILibrary.Models.ServiceProviderModel sp = new HandymanUILibrary.Models.ServiceProviderModel();
            HandymanUILibrary.Models.ProvidersServiceModel  ps = new HandymanUILibrary.Models.ProvidersServiceModel();
            string singleservice = "";
           

           
            

            _apiHepler = new APIHelper();
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
                Token = results.Access_Token;//Token
                var loggeduser = await _apiHepler.GetLoggedInUserInfor(Token);// awaited loggedUser
                UserId = loggeduser.Id;//pass user ID
                sp.UserId = UserId;

                HandymanUILibrary.Models.ServiceProviderModel sprov = new HandymanUILibrary.Models.ServiceProviderModel();
                try
                {
                   

                    await registerProvider.PostServiceProvider(sp);
                    sprov = await registerProvider.GetServiceProviders(sp);

                    ps.ServiceProviderId = sprov.Id;
                    await registerProvider.PostProvidersService(ps);
                    Session["providername"] = Token;
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
          
            ViewBag.Username = UserEmail;
            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "Your application description page.";
            ViewBag.Username = UserEmail;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Username = UserEmail;
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
                _apiHepler = new APIHelper();
                try
                {
                    var results = await _apiHepler.AuthenticateUser(Username, Password);
                    Token = results.Access_Token;
                   
                    UserEmail = Username;
                   
                    Session["log"] = Username;
                    Session["profilename"] = Token;
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }
               
               
            }
            ViewBag.Username = UserEmail;

            return View();
           
        }


        //public async Task SaveUser()
        //{
        //    RegisterEndPoint registerUser = new RegisterEndPoint(_apiHepler);

        //    try
        //    {
        //        _apiHepler = new APIHelper();
        //        registerUser = new RegisterEndPoint(_apiHepler);
        //        var results = await registerUser.SaveNewUser(UserEmail);
        //        RedirectToAction("CreateProfile");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.ErrorMsgCode = ex.Message;
        //    }


        //}

        //Create a profile action method
        public async Task<ActionResult> CreateProfile(Models.ProfileModel profile)
        {

            HandymanUILibrary.Models.ProfileModel profileModel = new HandymanUILibrary.Models.ProfileModel();
            profileModel.Name = profile.Name;
            profileModel.Surname = profile.Surname;
            profileModel.PhoneNumber = profile.PhoneNumber;
            profileModel.HomeAddress = profile.HomeAddress;
            profileModel.DateofBirth = profile.DateTimeOfBirth;
            profileModel.Type = "user";
            ViewBag.profilename = profile.Name + profile.Surname ;
           


            if (ModelState.IsValid)
            {
                if (_apiHepler == null)
                {
                    _apiHepler = new APIHelper();
                    profileEndPoint = new ProfileEndPoint(_apiHepler);


                    try
                    {
                        //We await multiple tasks to create the profile
                        //First Get the token with auth and get the loggedin user
                        //Then pass the user ID but keep it to track the user 
                        //Finally we await the profile post task and if successful redict to the mentioned view/method
                        var results = await _apiHepler.AuthenticateUser(Username, Password);//auth awaited task
                        Token = results.Access_Token;//Token
                        var loggeduser = await _apiHepler.GetLoggedInUserInfor(Token);// awaited loggedUser
                        profileModel.UserId = loggeduser.Id;//pass user ID
                        PhoneNumber = profile.PhoneNumber;
                        UserId = loggeduser.Id;//track user
                        await profileEndPoint.PostProfile(profileModel);//waited results of posted user profile
                        Session["profilename"] = Token;
                        return RedirectToAction("Index");

                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMsg = ex.Message;
                    }
                }
                else
                {
                    var loggeduser = await _apiHepler.GetLoggedInUserInfor(Token);
                    profileModel.UserId = loggeduser.Id;

                    await profileEndPoint.PostProfile(profileModel);
                    return RedirectToAction("Index");
                }
                

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

                    user.Email = newUser.Username;
                    UserEmail = newUser.Username;
                    Username = UserEmail;
                    Password = newUser.Password;
                    user.Password = newUser.Password;
                    user.ConfirmPassword = newUser.ConfirmPassword;
                    _apiHepler = new APIHelper();

                    RegisterEndPoint registerUser = new RegisterEndPoint(_apiHepler);

                
                    var result = await registerUser.RegisterUser(user);
                   
                    var results = await registerUser.SaveNewUser(user);
                    
                    UserEmail = Username;
                    //await SaveUser();
                    Session["log"] = Username;
                    


                    return RedirectToAction("CreateProfile");
                    
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
                if (oTPGenerator==null)
                {

                    oTPGenerator = new OTPGenerator();
                    var result = await oTPGenerator.CreateOTPEnvironment(UserId, PhoneNumber, Username);
                    
                    try
                    {
                        //await the OTP to validate
                        var lastValidation = await oTPGenerator.ValidateOTP(otmodel.OTP);
                        ViewBag.ErrorMsgCode = lastValidation.messageDescription;
                        return RedirectToAction("Index");

                    }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                }
                    else
                    {
                        var lastValidation = await oTPGenerator.ValidateOTP(otmodel.OTP);
                        ViewBag.ErrorMsgCode = lastValidation.messageDescription;
                        return RedirectToAction("Index");
                    }

            }

            return View();
        }

        //Loggin out method
        public ActionResult Logout()
        {
            Token = null;
            Session["providername"] = null;
            Session["profilename"] = null;
            _apiHepler = new APIHelper();
            _apiHepler.LogOutuser();
            Session["log"] = null;
            _apiHepler = null;          
               return RedirectToAction("Index");
          
        }
    }
}