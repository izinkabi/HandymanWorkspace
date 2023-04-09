using Handyman_SP_UI.Areas.Identity.Data;
using Handyman_SP_UI.Pages.Helpers;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handyman_SP_UI.Areas.Identity.Pages.Account
{
    [Authorize]
    public class WorkShopRegisterModel : PageModel
    {

        private IServiceEndpoint _servicesEP;
        private readonly RoleManager<IdentityRole> roleManager;
        private IBusinessHelper _businessHLP;
        private SignInManager<Handyman_SP_UIUser> SignInManager;
        private UserManager<Handyman_SP_UIUser> UserManager;
        private IProviderHelper _providerHelper;

        private IList<ServiceModel>? services;
        private BusinessModel? newBusiness = new();
        private string? ErrorMsg;

        [BindProperty]
        public bool IsWorkShopSaved { get; set; }
        [BindProperty]
        public bool IsServiceAdded { get; set; }
        [BindProperty]
        public bool IsOnSelectService { get; set; }

        public WorkShopRegisterModel(IServiceEndpoint servicesEP, RoleManager<IdentityRole> RoleManager,
            IBusinessHelper businessHLP,
            SignInManager<Handyman_SP_UIUser> signInManager,
            UserManager<Handyman_SP_UIUser> userManager,
            IProviderHelper providerHelper)
        {
            _servicesEP = servicesEP;
            roleManager = RoleManager;
            _businessHLP = businessHLP;
            SignInManager = signInManager;
            UserManager = userManager;
            _providerHelper = providerHelper;
        }
        [BindProperty]
        public List<ServiceModel> ServicesList { get { return services.ToList(); } set { services = value; } }
        [BindProperty]
        public BusinessModel NewBusiness { get { return newBusiness; } set { newBusiness = value; } }
        [BindProperty]
        public List<int> Selecteds { get; set; } = new List<int>();
        [BindProperty]
        public List<string> Errors { get; set; }
        public async void OnGet()
        {
            newBusiness.date = DateTime.Now;
            IsOnSelectService = false;

        }
        //Display Services
        private void OpenService()
        {
            IsOnSelectService = true;
        }

        [BindProperty]
        public int WorkShopType { get; set; }

        //Submit WorkShop Details
        public async Task<ActionResult> OnPostSaveWorkShopDetails()
        {
            if (ModelState.IsValid)
            {
                ServiceProviderModel newMember = new();
                if (newBusiness != null)
                {
                    try
                    {
                        //Get the registered user/provider
                        newMember.employeeProfile = await _providerHelper.GetProviderProfile();
                        newMember.IsOwner = true;
                        newMember.pro_providerId = newMember.employeeProfile.UserId;
                        //Get services
                        if (services == null || services.Count == 0)
                        {
                            services = await _servicesEP.GetServices();
                        }

                        //Save the owner of the workshop
                        if (newBusiness.Employees.Count == 0 && newMember.employeeProfile != null)
                        {
                            newBusiness.Employees.Add(newMember);
                        }

                        newBusiness.Type = WorkShopType;
                        newBusiness.date = DateTime.Now;
                        newBusiness.registration.name = newBusiness.Name;
                        newBusiness.registration.businessType = WorkShopType;
                        await _businessHLP.CreateBusiness(newBusiness);

                        if (await AssignOwnerToRole())
                        {
                            return RedirectToPage("/Index", "Home");
                        }
                        else
                        {
                            return Page();
                        }

                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = "Error:" + ex.Message;
                    }


                }
            }
            IsWorkShopSaved = true;
            OpenService();
            return Page();
        }

        private async Task<bool> AssignOwnerToRole()
        {
            try
            {
                //Shall be simplified
                var User = SignInManager.Context.User;
                var user = await UserManager.GetUserAsync(User);
                if (User.IsInRole("Owner"))
                {
                    return true;
                }
                if (!User.IsInRole("Owner"))
                {
                    if (!(await roleManager.RoleExistsAsync("Owner")))
                    {
                        IdentityResult identityResult = await roleManager.CreateAsync(new IdentityRole("Owner"));
                    }

                    if (await roleManager.RoleExistsAsync("Owner"))
                    {
                        var result = await UserManager.AddToRoleAsync(user, "Owner");
                        if (result.Succeeded)
                        {
                            await SignInManager.RefreshSignInAsync(user);

                            await AssignMemberToRole();
                            await SignInManager.RefreshSignInAsync(user);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }


                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Create the Owner as a member(a handymany)
        /// </summary>
        /// <returns>bool</returns>
        private async Task<bool> AssignMemberToRole()
        {
            if (Errors != null && Errors.Count > 0)
            {
                Errors.Clear();
            }

            //Shall be simplified
            var User = SignInManager.Context.User;
            var user = await UserManager.GetUserAsync(User);

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
                        var result = UserManager.AddToRoleAsync(user, "Member");

                        if (result.IsCompletedSuccessfully)
                        {
                            await SignInManager.RefreshSignInAsync(user);//Refresh the user to update the role
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
        //Add the WorkShop Services
        public async Task<ActionResult> OnPostSaveServices()
        {
            if (Selecteds.Count > 0)
            {
                ServiceProviderModel provider = new();
                provider = await _providerHelper.GetProvider();
                //Get Services
                if (services == null || services.Count == 0)
                {
                    services = await _servicesEP.GetServices();
                }

                foreach (var s in Selecteds)
                {

                    foreach (var service in services)
                    {
                        if (s == service.id)
                        {
                            provider.Services.Add(service);//Owner Picked Service

                        }
                    }
                }

                if (provider.Services.Count > 0)
                {
                    newBusiness.Employees.Add(provider);
                    foreach (var service in provider.Services)
                    {
                        await _providerHelper.AddService(service);//Add the services to workshop by owner
                    }

                }
                IsOnSelectService = false;
                IsServiceAdded = true;

            }
            return Redirect("~/");
        }


    }
}
