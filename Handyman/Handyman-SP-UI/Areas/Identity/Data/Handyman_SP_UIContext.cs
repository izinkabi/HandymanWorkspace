using Handyman_SP_UI.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Handyman_SP_UI.Data;

public class Handyman_SP_UIContext : IdentityDbContext<Handyman_SP_UIUser>
{
    public Handyman_SP_UIContext(DbContextOptions<Handyman_SP_UIContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
