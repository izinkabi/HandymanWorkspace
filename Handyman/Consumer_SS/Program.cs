using Consumer_SS.Helpers;
using Consumer_SS.Helpers.Tasks;
using HandymanUILibrary.API.Auth;
using HandymanUILibrary.API.Consumer.Order.Implementation;
using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.API.Services;
using HandymanUILibrary.API.User;
using HandymanUILibrary.Models.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using SS_UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<IAPIHelper, APIHelper>();
builder.Services.AddScoped<IServiceEndPoint, ServiceEndPoint>();
builder.Services.AddScoped<IOrderEndPoint, OrderEndPoint>();
builder.Services.AddScoped<IOrderHelper, OrderHelper>();
builder.Services.AddScoped<ITasksEndPoint, TasksEndPoint>();
builder.Services.AddScoped<ITasksHelper, TasksHelper>();
builder.Services.AddScoped<IAuthEndpoint, AuthEndpoint>();
builder.Services.AddScoped<AuthenticatedUserModel>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddResponseCompression(opt =>
{
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
      new[] { "application/octet-stream" });
});
builder.Services.AddAuthorizationCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
