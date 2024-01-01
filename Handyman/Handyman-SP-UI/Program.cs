using Handyman_SP_UI.Helpers;
using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Implementation;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;


var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Development
});


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IAPIHelper, APIHelper>();

builder.Services.AddScoped<IWorkshopEndPoint, WorkshopEndPoint>();
builder.Services.AddScoped<AuthenticatedUserModel>();
builder.Services.AddTransient<EmployeeEndPoint>();
builder.Services.AddScoped<IMemberEndpoint, MemberEndpoint>();
builder.Services.AddTransient<IOrderEndpoint, OrderEndpoint>();
builder.Services.AddScoped<IServiceEndpoint, ServiceEndpoint>();
builder.Services.AddScoped<IAuthEndpoint, AuthEndpoint>();
builder.Services.AddScoped<INegotiationEndPoint, NegotiationEndPoint>();
builder.Services.AddScoped<IWorkshopHelper, WorkshopHelper>();
builder.Services.AddScoped<IOrderHelper, OrderHelper>();
builder.Services.AddScoped<IMemberHelper, MemberHelper>();


builder.Services.AddScoped<AuthenticationStateProvider, CustomeAuthStateProvider>();

builder.Services.AddResponseCompression(opt =>
{
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
      new[] { "application/octet-stream" });
});
builder.Services.AddAuthorizationCore();


var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication()//.AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
//    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
//})
;



var app = builder.Build();


// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
