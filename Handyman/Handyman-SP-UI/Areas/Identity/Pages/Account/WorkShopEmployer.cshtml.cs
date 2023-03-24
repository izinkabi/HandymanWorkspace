using Handyman_SP_UI.Areas.Identity.Data;
using Handyman_SP_UI.Pages.Helpers;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handyman_SP_UI.Areas.Identity.Pages.Account
{
    public class WorkShopEmployerModel : PageModel
    {

        IBusinessHelper _workShopHelper;
        private readonly RoleManager<IdentityRole> roleManager;
        private UserManager<Handyman_SP_UIUser> userManager;
        private SignInManager<Handyman_SP_UIUser> signInManager;
        IProviderHelper _profileHelper;


        private BusinessModel? workShop = new();
        private string workShopRegistrationNumber;
        private static string ValidRegNumber;
        private List<ServiceModel> services;

        private bool isOnSelectService;
        private bool isBusinessFound;
        private bool isServiceAdded;

        public WorkShopEmployerModel(IBusinessHelper workShopHelper, RoleManager<IdentityRole> roleManager,
            UserManager<Handyman_SP_UIUser> userManager,
            SignInManager<Handyman_SP_UIUser> signInManager,
            IProviderHelper profileHelper)
        {
            _workShopHelper = workShopHelper;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _profileHelper = profileHelper;
        }


        [BindProperty]
        public BusinessModel WorkShopProperty { get { return workShop; } private set { workShop = value; } }

        [BindProperty]
        public List<string>? Errors { get; set; }

        [BindProperty]
        public string WorkShopRegistrationNumber { get { return workShopRegistrationNumber; } set { workShopRegistrationNumber = value; } }

        [BindProperty]
        public List<int> Selecteds { get; set; }
        [BindProperty]
        public bool IsOnSelectService { get { return isOnSelectService; } }
        [BindProperty]
        public bool IsBusinessFound { get { return isBusinessFound; } }
        [BindProperty]
        public bool IsServiceAdded { get { return isServiceAdded; } }

        [BindProperty]
        public List<ServiceModel> ServicesList
        {
            get { return services; }
            private set { services = value; }
        }

        private ServiceProviderModel newMember;
        [BindProperty]
        public ServiceProviderModel NewMemberProperty { get { return newMember; } }


        /// <summary>
        /// On Get Method
        /// </summary>
        /// <returns></returns>
        public void OnGet()
        {
            try
            {
                isOnSelectService = false;
                isBusinessFound = false;
                isJoinedMember = false;
            }
            catch (Exception ex)
            {
                Errors.Add($"Error:{ex.Message}");
                return;
            }


        }

        //Save services, can be a view component/partial-view
        public async Task<IActionResult> OnPostSaveServices()
        {
            if (Selecteds.Count > 0)
            {
                newMember = new();
                newMember.employeeProfile = await _profileHelper.GetProviderProfile();
                if (services is null || workShop is null)
                {
                    workShop = await GetWorkShop();
                }


                if (Selecteds != null && Selecteds.Count > 0)
                {
                    foreach (var s in Selecteds)
                    {
                        if (services != null && services.Count > 0)
                        {
                            foreach (var service in services)
                            {
                                if (s == service.id)
                                {
                                    newMember.Services.Add(service);//Owner Picked Service
                                }
                            }
                        }

                    }
                }
                if (newMember.Services.Count > 0)
                {
                    workShop.Employees.Add(newMember);
                    isServiceAdded = true;
                    isBusinessFound = true;

                }
            }
            return await OnPostSubmitJoin(newMember.Services);
        }

        //Get the Owner of the WOrkshop

        //Query Data System for Work Shop and their services
        private async Task<BusinessModel> GetWorkShop()
        {
            try
            {

                if (!string.IsNullOrEmpty(workShopRegistrationNumber))
                {
                    workShop = await _workShopHelper.GetWorkShop(workShopRegistrationNumber);
                    if (workShop != null && workShop.Employees.Count > 0)
                    {
                        services = new List<ServiceModel>();
                        services = workShop.Employees.First().Services;
                        ValidRegNumber = workShopRegistrationNumber;
                    }

                }
                else
                {
                    if (ValidRegNumber != null)
                    {
                        workShop = await _workShopHelper.GetWorkShop(ValidRegNumber);
                        services = new List<ServiceModel>();
                        services = workShop.Employees.First().Services;
                    }
                    else
                    {
                        Errors.Add("Error: Empty Work Shop Registration Number");
                    }

                }

                return workShop;
            }
            catch (Exception ex)
            {
                Errors = new();
                Errors.Add($"Error:{ex.Message}");
                throw new Exception(ex.Message);
            }


        }

        //Capture the WorkShop to join
        public async Task<IActionResult> OnPostFindWorkShop()
        {


            if (Errors != null && Errors.Count > 0)
            {
                Errors.Clear();
            }
            if (!string.IsNullOrEmpty(ValidRegNumber))
            {
                if (newMember is null)
                {
                    newMember = new();
                    newMember.BusinessId = (await GetWorkShop()).Employees.First().BusinessId;

                }

            }
            else
            {
                if (workShopRegistrationNumber != null)
                {
                    newMember = new();
                    newMember.BusinessId = (await GetWorkShop()).Employees.First().BusinessId;
                }
                else
                {
                    isBusinessFound = false;
                    isOnSelectService = false;
                    isServiceAdded = false;
                    Errors = new();
                    Errors.Add("Error: Some weird input just happened");
                }

            }
            isBusinessFound = true;
            isOnSelectService = true;

            return Page();
        }




        private async Task<bool> AssignMemberToRole()
        {
            if (Errors != null && Errors.Count > 0)
            {
                Errors.Clear();
            }

            //Shall be simplified
            var User = signInManager.Context.User;
            var user = await userManager.GetUserAsync(User);

            try
            {
                //If the user is assigned to role its's cool
                if (User.IsInRole("Member"))
                {
                    return true;
                }
                //Assign a new member to the role
                if (!User.IsInRole("Member"))
                {
                    //Create a non-existing role (should be in the AppManager)
                    //*****************@******************
                    if (!(await roleManager.RoleExistsAsync("Member")))
                    {
                        IdentityResult identityResult
                             = await roleManager.CreateAsync(new IdentityRole("Member"));
                    }

                    if (await roleManager.RoleExistsAsync("Member"))
                    {
                        var result = await userManager.AddToRoleAsync(user, "Member");

                        if (result.Succeeded)
                        {
                            await signInManager.RefreshSignInAsync(user);//Refresh the user to update the role
                            return true;
                        }

                    }
                    //******************@******************

                }

            }
            catch (Exception ex)
            {
                Errors.Add($"Error : {ex.Message}");
                return false;

            }
            return false;

        }

        /// <summary>
        /// Keep track of the new member's addition
        /// </summary>
        private bool isJoinedMember;

        [BindProperty]
        public bool IsJoinedMember
        {
            get { return isJoinedMember; }
        }

        //Submit WorkShop Join/Request To Join 
        public async Task<IActionResult> OnPostSubmitJoin(List<ServiceModel> pickServices)
        {
            if (Errors != null && Errors.Count > 0)
            {
                Errors.Clear();
            }
            try
            {
                if (workShop is null || workShop.Id is 0)
                {
                    workShop = await GetWorkShop();
                    workShopRegistrationNumber = null;
                    services = workShop.Employees.First().Services;
                }
                if (workShop != null && workShop.registration.Id != 0)
                {
                    //Create new provider
                    newMember = new();
                    //Get the recently created profile
                    newMember.employeeProfile = await _profileHelper.GetProviderProfile();
                    newMember.BusinessId = workShop.Id;
                    newMember.employeeId = newMember.employeeId;//This shows that Employee Id can be derived from Profile Id as the Profile can be derive from User 
                    newMember.IsOwner = false;
                    newMember.BusinessId = workShop.registration.Id;
                    newMember.pro_providerId = newMember.employeeProfile.UserId;
                    newMember.employeeId = newMember.pro_providerId;
                    newMember.Services = pickServices;

                    //Add the registered newMember to WorkShop
                    var result = await _workShopHelper.AddMemberToWorkShop(newMember);

                    if (result)
                    {

                        var roleInsertResult = await AssignMemberToRole();  //Assign Role
                        isJoinedMember = true;

                        if (roleInsertResult)
                        {
                            return Redirect("~/");
                        }
                        else { return Page(); }

                    }
                    else
                    {
                        Errors.Add("Error: The was an issue, try again!");
                        return Page();
                    }
                }
                return Page();

            }
            catch (Exception ex)
            {
                Errors.Add($"Error:{ex.Message}");
                return Page();
            }

        }
    }
}
