using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HandymanUIApp.Models;
using HandymanUILibrary.Models;

namespace HandymanUIApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HandymanUIApp.Models.OrderModel>? OrderModel { get; set; }
        public DbSet<HandymanUIApp.Models.ServiceModel>? ServiceModel { get; set; }
        public DbSet<HandymanUILibrary.Models.ServiceModel>? ServiceModel_1 { get; set; }
        
       
    }
}