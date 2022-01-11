using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Handyman_UI.Controllers
{
    public class ProfileController : Controller
    {

        private IAPIHelper _apiHepler;
        static private string Username,Password;
        static private string Token;
        static private string UserRole;
        
        private HandymanUILibrary.Models.ProfileModel profileModel;      
        private NewUserModel user;

        private IProfileEndPoint _profileEndPoint;
        private IRegisterEndPoint _registerEndPoint;
       
        static private string DisplayUserName;
        static private loggedInUserModel loggedInUserModel;
        public ProfileController(IAPIHelper aPIHelper,IProfileEndPoint profile,IRegisterEndPoint registerEndPoint)
        {
            _apiHepler = aPIHelper;
            _profileEndPoint = profile;
            _registerEndPoint = registerEndPoint;         
        }

        //Action method for 
        public PartialViewResult CreateAddress()
        {
            
            return PartialView();
        }
            
            //Register action method
    public async Task<ActionResult> Register(CreateUserModel newUser)
            {

            if (ModelState.IsValid)
            {
                try
                {

                   
                    user = new HandymanUILibrary.Models.NewUserModel();

                    //Username and password are given a value once here and once in sign in
            
                    user.Email = newUser.Username;
                    user.Password = newUser.Password;
                    user.ConfirmPassword = newUser.ConfirmPassword;

                    Username = newUser.Username;
                    Password = newUser.Password;
                    user.UserRole = "Customer";//User role assignment to customer
                    UserRole = "Customer";

                    var loggedInUser = await _registerEndPoint.RegisterUser(user);
                    Session["Token"] = loggedInUser.Access_Token;

                    return RedirectToAction("CreateAProfile", "Profile");

                }
                catch (Exception ex)
                {
                    ViewBag.RegisterErrorMsg = ex.Message;
                }

            }
            return View();
        }

    public async Task<ActionResult> RegisterProvider(CreateUserModel newUser)
    {

            if (ModelState.IsValid)
            {
                try
                {


                    user = new HandymanUILibrary.Models.NewUserModel();

                    //Username and password are given a value once here and once in sign in

                    user.Email = newUser.Username;
                    user.Password = newUser.Password;
                    user.ConfirmPassword = newUser.ConfirmPassword;

                    Username = newUser.Username;
                    Password = newUser.Password;
                    user.UserRole = "ServiceProvider";//User role assignment
                    UserRole = "ServiceProvider";
                    var loggedInUser = await _registerEndPoint.RegisterUser(user);
                    Session["Token"] = loggedInUser.Access_Token;


                    return RedirectToAction("CreateAProfile", "Profile");

                }
                catch (Exception ex)
                {
                    ViewBag.RegisterErrorMsg = ex.Message;
                }

            }
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
                    loggedInUserModel = new loggedInUserModel();
                    var result = await _apiHepler.GetLoggedInUserInfor(results.Access_Token);
                    loggedInUserModel.Token = result.Token;
                    loggedInUserModel.Id = result.Id;
                    loggedInUserModel.UserRole = result.UserRole;

                    Session["log"] = results.Access_Token;
                    Token = results.Access_Token;
                    TempData["welcome"] = "Welcome " + Username;
                    Session["Username"] = Username;
                    Session["Token"]=results.Access_Token;

                    //////this will brake my code--(! Illegal assignment of user-roles from aspnetdb, less secured...  )
                    if (loggedInUserModel.UserRole.Equals("Customer"))

                    {
                        return RedirectToAction("Index", "Service");

                    }
                    else if (loggedInUserModel.UserRole.Equals("ServiceProvider"))
                    {
                        return RedirectToAction("Home", "ServiceProviderHome");
                    }


                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }


            }

           
            return View();

    }
    

    public async Task<ActionResult> CreateAProfile(Models.ProfileDisplayModel profile, Models.ProfileDisplayModel.AddressModel address )
        {

            if (ModelState.IsValid)
            {
                //this should be enough to pass to the API endpoint
                profileModel = new HandymanUILibrary.Models.ProfileModel();
                profileModel.Address = new HandymanUILibrary.Models.ProfileModel.AddressModel();
               
                profileModel.Name = profile.Name;
                profileModel.Surname = profile.Surname;
                profileModel.PhoneNumber = profile.PhoneNumber;
                profileModel.DateOfBirth = profile.DateOfBirth;
               
                profileModel.Address.StreetName = address.StreetName;
                profileModel.Address.Surburb = address.Surburb;
                profileModel.Address.PostalCode = address.PostalCode;
                profileModel.Address.HouseNumber = address.HouseNumber;
                profileModel.Address.City = address.City;

                ViewBag.profilename = profile.Name + profile.Surname;
               

                try
                {
                    //We await multiple tasks to create the profile
                    //First Get the token with auth and get the loggedin user
                    //Then pass the user ID but keep it to track the user 
                    //Finally we await the profile post task and if successful redict to the mentioned view/method


                    var results = await _apiHepler.AuthenticateUser(Username, Password);//auth awaited task
                    Token = results.Access_Token;
                    Session["Token"] = results.Access_Token;
                    var loggeuser = await _apiHepler.GetLoggedInUserInfor(results.Access_Token);// awaited loggedUser

                    profileModel.UserId = loggeuser.Id;//pass user ID

                    await _profileEndPoint.PostProfile(profileModel);//waited results of posted user profile

                    if (UserRole=="Customer")
                    {
                       
                        return RedirectToAction("RegisterCustomer", "CustomerHome");

                    }
                    else if (UserRole=="ServiceProvider" )
                    {
                        TempData["newuser"] = "Service Provider";
                        return RedirectToAction("RegisterServiceProvider", "ServiceProviderHome");
                    }

                    

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }

                

            }

            return View();
        }

    public ActionResult AddressDetails()
    {
        return PartialView();
    }
    public async Task<ActionResult> ProfileDetails()
        {
            try
            {
                var token = Session["Token"].ToString();
                var loggeduser = await _apiHepler.GetLoggedInUserInfor(token);
                var user = new UserModel();
                user.Id = loggeduser.Id;
                //Getting a profile with its Address
                var results =  await _profileEndPoint.GetProfile(user);

                var tempProfile = new Models.ProfileDisplayModel();
                tempProfile.AddressM = new Models.ProfileDisplayModel.AddressModel();

                tempProfile.ProfileId = results.Id;
                tempProfile.Name = results.Name;
                tempProfile.Surname = results.Surname;
                tempProfile.PhoneNumber = results.PhoneNumber;
                tempProfile.UserId = results.UserId;
                tempProfile.DateOfBirth = results.DateOfBirth;
                
                /*Address population*/
                tempProfile.AddressM.Id = results.Address.Id;
                tempProfile.AddressM.StreetName = results.Address.StreetName;
                tempProfile.AddressM.HouseNumber = results.Address.HouseNumber;
                tempProfile.AddressM.Surburb = results.Address.Surburb;
                tempProfile.AddressM.PostalCode = results.Address.PostalCode;
                tempProfile.AddressM.City = results.Address.City;


                return View(tempProfile);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
            }

            return View();
        }


        //The two following methods edit the customer's profile
        // GET: /Profile/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var token = Session["Token"].ToString();
                var loggedUser = await _apiHepler.GetLoggedInUserInfor(token);
                UserModel user = new UserModel();
                user.Id = loggedUser.Id;
                var profile = await _profileEndPoint.GetProfile(user);

                ProfileDisplayModel profileDisplayModel = new ProfileDisplayModel();
                profileDisplayModel.UserId = profile.UserId;
                profileDisplayModel.ProfileId = profile.Id;
                profileDisplayModel.Name = profile.Name;
                profileDisplayModel.Surname = profile.Surname;
                profileDisplayModel.DateOfBirth = profile.DateOfBirth;
                profileDisplayModel.PhoneNumber = profile.PhoneNumber;

                profileDisplayModel.AddressM = new ProfileDisplayModel.AddressModel();
                profileDisplayModel.AddressM.Id = profile.Address.Id;
                profileDisplayModel.AddressM.City = profile.Address.City;
                profileDisplayModel.AddressM.PostalCode = profile.Address.PostalCode;
                profileDisplayModel.AddressM.StreetName = profile.Address.StreetName;
                profileDisplayModel.AddressM.Surburb = profile.Address.Surburb;
                profileDisplayModel.AddressM.HouseNumber = profile.Address.HouseNumber;
                if (profile == null)
                {
                    return HttpNotFound();
                }
                return View("Edit", profileDisplayModel);
            }
         
            
        }

        // POST: /Profile/Edit/5    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProfileDisplayModel profile)
        {
            if (ModelState.IsValid)
            {
                var profileUpdate = new ProfileModel();

                profileUpdate.Address = new ProfileModel.AddressModel();
                profileUpdate.Id = profile.ProfileId;
                profileUpdate.Address.City = profile.AddressM.City;
                profileUpdate.Address.StreetName = profile.AddressM.StreetName;
                profileUpdate.Address.Surburb = profile.AddressM.Surburb;
                profileUpdate.Address.PostalCode = profile.AddressM.PostalCode;
                profileUpdate.Address.HouseNumber = profile.AddressM.HouseNumber;
                profileUpdate.Address.Id = profile.AddressM.Id;

                profileUpdate.UserId = profile.UserId;
                profileUpdate.Name = profile.Name;
                profileUpdate.PhoneNumber = profile.PhoneNumber;
                profileUpdate.Surname = profile.Surname;
                profileUpdate.DateOfBirth = profile.DateOfBirth;
                TempData["updatedprofile"] = profile.Name + " you edited your Profile";
                //first update the profile
                await _profileEndPoint.UpdateProfile(profileUpdate);
                return RedirectToAction("ProfileDetails");
            }
            return View(profile);
        }
    }
}