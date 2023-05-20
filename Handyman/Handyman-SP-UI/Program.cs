using Handyman_SP_UI;
using Handyman_SP_UI.Hubs;
using Handyman_SP_UI.Pages.Helpers;
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

builder.Services.AddScoped<IBusinessEndPoint, BusinessEndPoint>();
builder.Services.AddScoped<AuthenticatedUserModel>();
builder.Services.AddTransient<EmployeeEndPoint>();
builder.Services.AddTransient<IServiceProviderEndPoint, ServiceProviderEndPoint>();
builder.Services.AddTransient<IRequestEndPoint, RequestEndPoint>();
builder.Services.AddScoped<IServiceEndpoint, ServiceEndpoint>();
builder.Services.AddScoped<IAuthEndpoint, AuthEndpoint>();
builder.Services.AddScoped<INegotiationEndPoint, NegotiationEndPoint>();
builder.Services.AddScoped<IBusinessHelper, BusinessHelper>();
builder.Services.AddScoped<IRequestHelper, RequestHelper>();
builder.Services.AddScoped<IProviderHelper, ProviderHelper>();


builder.Services.AddScoped<AuthenticationStateProvider, CustomeAuthStateProvider>();

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

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<NegotiationHub>("/negoHub");
app.MapHub<RequestHub>("/reqHub");
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
