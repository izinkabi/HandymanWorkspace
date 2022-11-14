using Handyman_SP_UI.Areas.Identity;
using Handyman_SP_UI.Areas.Identity.Data;
using Handyman_SP_UI.Data;
using Handyman_SP_UI.Pages.Helpers;
using HandymanProviderLibrary.Api.Business.Implementation;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.API;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Handyman_SP_UIContextConnection") ?? throw new InvalidOperationException("Connection string 'Handyman_SP_UIContextConnection' not found.");

builder.Services.AddDbContext<Handyman_SP_UIContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Handyman_SP_UIUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Handyman_SP_UIContext>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IAPIHelper, APIHelper>();
builder.Services.AddScoped<IBusinessEndPoint, BusinessEndPoint>();
builder.Services.AddScoped<EmployeeEndPoint>();
builder.Services.AddScoped<IServiceProviderEndPoint, ServiceProviderEndPoint>();
builder.Services.AddScoped<IBusinessEndPoint, BusinessEndPoint>();
builder.Services.AddScoped<IBusinessHelper, BusinessHelper>();
builder.Services.AddAntiforgery();
builder.Services.AddScoped<TokenProvider>();

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
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();


app.Run();
