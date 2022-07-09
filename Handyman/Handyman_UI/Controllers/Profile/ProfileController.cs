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

        static private string UserRole;
        private ProfileModel profileModel; 
        private IProfileEndPoint _profileEndPoint;
        string ErrorMsg;
       
        private IAPIHelper _apiHelper;
       
        
        public ProfileController(IAPIHelper aPIHelper, IProfileEndPoint profile,IloggedInUserModel LoggedInUserModel)
        {
            _apiHelper = aPIHelper;
            _profileEndPoint = profile;
           // _loggedInUserModel = LoggedInUserModel;
        }
        /// <summary>
        /// Partial view of Address Creation
        /// </summary>
        /// <returns></returns>
        public PartialViewResult CreateAddress()
        {
            return PartialView();
        }
        /// <summary>
        /// Create Profile Funtion
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<ActionResult> CreateAProfile(ProfileModel profile,ProfileModel.AddressModel address)
        {

            if (ModelState.IsValid && profile.Name!=null)
            {

                //ViewBag.profilename = profile.Name + profile.Surname;//display the profile names
                
                try
                {
                    if (Session["loggedinuser"] != null)
                    {
                        var loggeduser = (loggedInUserModel)Session["loggedinuser"];
                        if(loggeduser != null && loggeduser is loggedInUserModel)
                        profileModel = profile;
                        profileModel.Address = address;
                        profileModel.UserId = loggeduser.Id;//pass user ID
                        UserRole = loggeduser.UserRole;
                        await _profileEndPoint.PostProfile(profileModel);//waited results of posted user profile
                        Session["loggedinprofile"] = await _profileEndPoint.GetProfile(loggeduser.Id);
                    }
                       
                        if (UserRole == "Customer")
                        {
                            return RedirectToAction("RegisterCustomer", "CustomerHome");
                        }
                        else if (UserRole == "ServiceProvider")
                        {
                            
                            return RedirectToAction("RegisterServiceProvider", "ServiceProviderHome");
                        }
                    }
                
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                    ErrorMsg = ex.Message;
                }

                
            }

            return View();
        }
        /// <summary>
        /// Partial View of Profile Display
        /// </summary>
        /// <returns></returns>
        public ActionResult AddressDetails()
        {
            return PartialView();
        }
        /// <summary>
        /// Display Profile Function
        /// </summary>
        /// <returns></returns>
        public  ActionResult ProfileDetails()
        {

            if (ModelState.IsValid)
            {


                try
                {
                   profileModel = (ProfileModel)Session["loggedinprofile"];
                    if (profileModel != null)
                    {
                        return View(profileModel);
                    }
                }

                catch (Exception ex)
                {
                    ErrorMsg = ex.Message;
                }
            }
            return View();
        }

        /// <summary>
        /// This methods finds the details of a Profile to be editted/modified
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            } 
            else
            {
                try
                {
                    
                    profileModel = (ProfileModel)Session["loggedinprofile"];
                    
                    if (profileModel != null && profileModel is ProfileModel) 
                    {
                        return View("Edit", profileModel);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);                   
                }
               
            }
         
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        /// <summary>
        /// This method Updates the Profile details that were editted 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ActionResult> Edit(ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                    //first update the profile
                    if (profile != null && profile is ProfileModel)
                    {
                        await _profileEndPoint.UpdateProfile(profile);
                        Session["loggedinprofile"] = profile;
                        TempData["updatedprofile"] = profile.Name + " you edited your Profile";
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return RedirectToAction("ProfileDetails");
            }
            return View(profile);
        }
    }
}