using HandymanUIApp.Data;
using HandymanUILibrary.API;
using HandymanUILibrary.API.Consumer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IAPIHelper,APIHelper>();
builder.Services.AddScoped<IServiceEndPoint, ServiceEndPoint>();
builder.Services.AddTransient<IConsumerEndPoint, ConsumerEndPoint>();
builder.Services.AddTransient<IOrderEndPoint, OrderEndPoint>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Services}/{action=ServiceHome}/{id?}");
//app.MapControllerRoute(
//    name: "ordersRoute",
//    pattern: "{controller=Orders}/{action=ConfirmOrder}/{id?}");
//app.MapControllerRoute(
//    name: "ordersCreateRoute",
//    pattern: "{controller=Orders}/{action=CreateOrder}/{id?}");
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
