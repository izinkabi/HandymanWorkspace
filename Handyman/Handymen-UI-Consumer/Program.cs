using HandymanUILibrary.API;
using HandymanUILibrary.API.Consumer.Order.Implementation;
using HandymanUILibrary.API.Consumer.Order.Interface;
using Handymen_UI_Consumer.Areas.Identity.Data;
using Handymen_UI_Consumer.Data;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Handymen_UI_ConsumerContextConnection") ?? throw new InvalidOperationException("Connection string 'Handymen_UI_ConsumerContextConnection' not found.");

builder.Services.AddDbContext<Handymen_UI_ConsumerContext>(options =>   
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Handymen_UI_ConsumerUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Handymen_UI_ConsumerContext>();


builder.Services.AddSingleton<IAPIHelper, APIHelper>();
builder.Services.AddTransient<IServiceEndPoint, ServiceEndPoint>();
builder.Services.AddTransient<IOrderEndPoint, OrderEndPoint>();
builder.Services.AddScoped<IOrderHelper, OrderHelper>();

//External Login
builder.Services.AddAuthentication().AddGoogle(googleOptions=>{
    googleOptions.ClientId = "1073851415525-6b5javce4n7umbggs3pie8ubfid3mbb0.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
});
//Configure Identity to customize settings
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddResponseCompression(opt =>
{
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
      new[] { "application/octet-stream" });
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapBlazorHub();

app.MapFallbackToPage("/_Host");


app.Run();
