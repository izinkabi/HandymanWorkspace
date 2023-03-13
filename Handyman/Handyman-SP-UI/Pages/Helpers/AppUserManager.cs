using Handyman_SP_UI.Areas.Identity.Data;
using Handyman_SP_UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Handyman_SP_UI.Pages.Helpers
{

    public class AppUserManager : UserManager<Handyman_SP_UIUser>
    {
        private readonly UserStore<Handyman_SP_UIUser, IdentityRole, Handyman_SP_UIContext, string, IdentityUserClaim<string>,
            IdentityUserRole<string>, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>> _store;

        public AppUserManager(
            IUserStore<Handyman_SP_UIUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<Handyman_SP_UIUser> passwordHasher,
            IEnumerable<IUserValidator<Handyman_SP_UIUser>> userValidators,
            IEnumerable<IPasswordValidator<Handyman_SP_UIUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<Handyman_SP_UIUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _store = (UserStore<Handyman_SP_UIUser, IdentityRole, Handyman_SP_UIContext, string, IdentityUserClaim<string>,
                IdentityUserRole<string>, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>)store;
        }

        public virtual async Task<IdentityResult> AddToRoleByRoleIdAsync(Handyman_SP_UIUser user, string roleId)
        {
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleId))
                throw new ArgumentNullException(nameof(roleId));

            if (await IsInRoleByRoleIdAsync(user, roleId, CancellationToken))
                return IdentityResult.Failed(ErrorDescriber.UserAlreadyInRole(roleId));

            _store.Context.Set<IdentityUserRole<string>>().Add(new IdentityUserRole<string> { RoleId = roleId, UserId = user.Id });

            return await UpdateUserAsync(user);
        }

        public async Task<bool> IsInRoleByRoleIdAsync(IdentityUser user, string roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleId))
                throw new ArgumentNullException(nameof(roleId));

            var role = await _store.Context.Set<IdentityRole>().FindAsync(roleId);
            if (role == null)
                return false;

            var userRole = await _store.Context.Set<IdentityUserRole<string>>().FindAsync(new object[] { user.Id, roleId }, cancellationToken);
            return userRole != null;
        }

        //private async Task CreateRolesandUsers()
        //{
        //    bool x = await _roleManager.RoleExistsAsync("Admin");
        //    if (!x)
        //    {
        //        // first we create Admin rool    
        //        var role = new IdentityRole();
        //        role.Name = "Admin";
        //        await _roleManager.CreateAsync(role);

        //        //Here we create a Admin super user who will maintain the website                   

        //        var user = new ApplicationUser();
        //        user.UserName = "default";
        //        user.Email = "default@default.com";

        //        string userPWD = "somepassword";

        //        IdentityResult chkUser = await _userManager.CreateAsync(user, userPWD);

        //        //Add default User to Role Admin    
        //        if (chkUser.Succeeded)
        //        {
        //            var result1 = await _userManager.AddToRoleAsync(user, "Admin");
        //        }
        //    }

        //    // creating Creating Manager role     
        //    x = await _roleManager.RoleExistsAsync("Manager");
        //    if (!x)
        //    {
        //        var role = new IdentityRole();
        //        role.Name = "Manager";
        //        await _roleManager.CreateAsync(role);
        //    }

        //    //public void creating Creatingmployee role     
        //    x = await _roleManager.RoleExistsAsync("Employee");
        //    if (!x)
        //    {
        //        var role = new IdentityRole();
        //        role.Name = "Employee";
        //        await _roleManager.CreateAsync(role);
        //    }
        //}
    }
}
