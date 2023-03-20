using Handyman_SP_UI.Areas.Identity.Data;
using Handyman_SP_UI.Pages.Helpers;
using HandymanProviderLibrary.Api.Service;
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

        public WorkShopRegisterModel(IServiceEndpoint servicesEP,
            IBusinessHelper businessHLP,
            SignInManager<Handyman_SP_UIUser> signInManager,
            UserManager<Handyman_SP_UIUser> userManager,
            IProviderHelper providerHelper)
        {
            _servicesEP = servicesEP;
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

                        var User = SignInManager.Context.User;
                        var user = await UserManager.GetUserAsync(User);

                        if (User.IsInRole("Owner"))
                        {
                            return RedirectToPage("./Index", "Home");
                        }
                        else
                        {
                            var result = await UserManager.AddToRoleAsync(user, "Owner");

                            await SignInManager.RefreshSignInAsync(user);//Refresh the user to update the role

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
                    await _providerHelper.AddService(provider);//Add the services to workshop by owner
                }
                IsOnSelectService = false;
                IsServiceAdded = true;

            }
            return Redirect("~/");
        }


    }
}
