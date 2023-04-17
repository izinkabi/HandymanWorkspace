using Handyman_SP_UI.Areas.Identity.Data;
using Handyman_SP_UI.Pages.Helpers;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handyman_SP_UI.Areas.Identity.Pages.Account.Registration.MembersRegistration
{
    [Authorize]
    public class ProfileRegisterModel : PageModel
    {
        private ServiceProviderModel provider = new()!;
        internal bool IsProfileSaved;
        internal bool IsServicesSaved;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private AuthenticationStateProvider authProvider;
        private UserManager<Handyman_SP_UIUser> userManager;
        private SignInManager<Handyman_SP_UIUser> signInManager;

        IProviderHelper _providerHelper;
        IConfiguration _config;

        public ProfileRegisterModel(IProviderHelper providerHelper, IConfiguration config,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment,
            AuthenticationStateProvider authState,
            SignInManager<Handyman_SP_UIUser> signInManager,
            UserManager<Handyman_SP_UIUser> manager)
        {
            _providerHelper = providerHelper;
            _config = config;
            _environment = environment;
            authProvider = authState;
            this.signInManager = signInManager;

            userManager = manager;
        }

        [BindProperty]
        public ServiceProviderModel ProviderProperty { get { return provider; } set { provider = value; } }
        public void OnGet()
        {
            provider.employeeProfile = new();
            provider.employeeProfile.DateOfBirth = DateTime.Now;
            IsProfileSaved = false;
        }

        internal void SelectedGender(string e)
        {
            if (e is not null)
            {
                switch (e)
                {
                    case "1":
                        provider.employeeProfile.Gender = "Male";

                        break;
                    case "2":
                        provider.employeeProfile.Gender = "Female";
                        break;
                    case "3":
                        provider.employeeProfile.Gender = "Other";
                        break;

                }


            }
        }

        private long MaxFileSize = 1024 * 1024 * 3;//3 megabytes
        private int MaxAllowFiles = 1;
        public List<string> errors = new();
        [BindProperty]
        public IFormFile? Upload { get; set; }




        //internal void OnChangeFileInputAsync(InputFileChangeEventArgs e)
        //{
        //    file = e.File;
        //}

        private async Task<string> CaptureImageFile()
        {


            if (Upload is null)
            {
                return "";
            }

            try
            {

                //Get the file name
                string newFileName = Path.ChangeExtension(
                    Path.GetRandomFileName(),
                    Path.GetExtension(Upload.Name));
                //Create file path / actual path
                string path = Path.Combine(
                    _config.GetValue<string>("FileStorage")!,
                    provider.employeeProfile.Names,
                    newFileName);

                //Get relative path

                string relativePath = Path.Combine(provider.employeeProfile.Names, newFileName);
                //Create a directory
                Directory.CreateDirectory(Path.Combine(
                _config.GetValue<string>("FileStorage")!,
                provider.employeeProfile.Names));

                await using FileStream fs = new(path, FileMode.Create);
                await Upload.OpenReadStream().CopyToAsync(fs);

                //var file = Path.Combine(_environment.ContentRootPath, Upload.FileName);
                //using (var fileStream = new FileStream(file, FileMode.Create))
                //{
                //    await Upload.CopyToAsync(fileStream);
                //}

                return relativePath;
            }
            catch (Exception ex)
            {
                errors.Add($"File: {Upload.Name} Error: {ex.Message}");
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        //string CreateWebPath(string relativePath)
        //{
        //    return Path.Combine(_config.GetValue<string>("WebStorageRoot")!,relativePath);
        //}
        void SaveServices(List<ServiceModel> services)
        {

            if (services != null && services.Count > 0)
            {
                provider.Services.AddRange(services);
                IsServicesSaved = true;
            }

        }


        public async Task<ActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {


                    provider.employeeProfile.ImageUrl = await CaptureImageFile();
                    SelectedGender(Request.Form["genderSelect"].ToString());
                    var user = signInManager.Context.User;
                    provider.employeeProfile.EmailAddress = user.Identity.Name;
                    provider.employeeProfile.UserId = userManager.GetUserId(user);


                   
                    IsProfileSaved = await _providerHelper.RegisterProfile(provider.employeeProfile); ;

                    if (IsProfileSaved)
                        return Redirect(Url.Page("/Account/WorkShopRegister"));
                    else
                        errors.Add("No profile Added , somthin weirdd happened!");
                        return Page();

                }
            }
            catch (Exception ex)
            {
                errors.Add($"Error:{ex.Message}");
            }
            return Page();
        }

        void EditProfile()
        {
            IsProfileSaved = false;

        }
    }
}
