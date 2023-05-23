using HandymanUILibrary.API.Auth;
using HandymanUILibrary.API.Consumer.Order.Implementation;
using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.API.Services;
using HandymanUILibrary.API.User;
using HandymanUILibrary.Models.Auth;
using Handymen_UI_Consumer.Helpers;
using Handymen_UI_Consumer.Helpers.Tasks;
using HandymenUIConsumer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);


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




builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer();
//    options => builder.Configuration.Bind("JwtSettings", options))
//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
//    options => builder.Configuration.Bind("CookieSettings", options));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
});

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
