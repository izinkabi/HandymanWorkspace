using HandymanUILibrary.API.Auth;
using HandymanUILibrary.API.Consumer.Order.Implementation;
using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.API.Services;
using HandymanUILibrary.API.User;
using HandymanUILibrary.Models.Auth;
using Handymen_UI_Consumer.Helpers;
using Handymen_UI_Consumer.Helpers.Tasks;
using HandymenUIConsumer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("Handymen_UI_ConsumerContextConnection") ?? throw new InvalidOperationException("Connection string 'Handymen_UI_ConsumerContextConnection' not found.");

//builder.Services.AddDbContext<Handymen_UI_ConsumerContext>(options =>
//    options.UseSqlServer(connectionString));

//builder.Services.AddIdentity<Handymen_UI_ConsumerUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<Handymen_UI_ConsumerContext>()
//    .AddDefaultUI();

builder.Services.AddSingleton<IAPIHelper, APIHelper>();
builder.Services.AddTransient<IServiceEndPoint, ServiceEndPoint>();
builder.Services.AddTransient<IOrderEndPoint, OrderEndPoint>();
builder.Services.AddScoped<IOrderHelper, OrderHelper>();
builder.Services.AddTransient<ITasksEndPoint, TasksEndPoint>();
builder.Services.AddTransient<ITasksHelper, TasksHelper>();
builder.Services.AddTransient<IloggedInUserModel, loggedInUserModel>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomeAuthStateProvider>();
builder.Services.AddScoped<IAuthEndpoint, AuthEndpoint>();
builder.Services.AddScoped<AuthenticatedUserModel>();


//External Login
//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = "1073851415525-6b5javce4n7umbggs3pie8ubfid3mbb0.apps.googleusercontent.com";
//    googleOptions.ClientSecret = "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";
//});
//Configure Identity to customize settings
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;

//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters =
//    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;
//});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("JwtSettings", options))
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("CookieSettings", options));

builder.Services.AddResponseCompression(opt =>
{
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
      new[] { "application/octet-stream" });
});

builder.Services.AddSignalR(e =>
{
    e.MaximumReceiveMessageSize = 102400000;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}



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
