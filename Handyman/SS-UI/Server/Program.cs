using HandymanUILibrary.API.Auth;
using HandymanUILibrary.API.Consumer.Order.Implementation;
using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.API.Services;
using HandymanUILibrary.API.User;
using HandymanUILibrary.Models.Auth;
using Microsoft.AspNetCore.ResponseCompression;
using SS_UI.Helpers;
using SS_UI.Helpers.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAPIHelper, APIHelper>();
builder.Services.AddTransient<IServiceEndPoint, ServiceEndPoint>();
builder.Services.AddTransient<IOrderEndPoint, OrderEndPoint>();
builder.Services.AddScoped<IOrderHelper, OrderHelper>();
builder.Services.AddTransient<ITasksEndPoint, TasksEndPoint>();
builder.Services.AddTransient<ITasksHelper, TasksHelper>();
builder.Services.AddScoped<IAuthEndpoint, AuthEndpoint>();
builder.Services.AddScoped<AuthenticatedUserModel>();
builder.Services.AddAuthorizationCore();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddResponseCompression(opt =>
{
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
      new[] { "application/octet-stream" });
});
builder.Services.AddAuthorizationCore();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
