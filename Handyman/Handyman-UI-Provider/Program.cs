using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Handyman_UI_Provider.Areas.Identity.Data;
using HandymanProviderLibrary.Api.Service;
using HandymanProviderLibrary.API;
using Microsoft.AspNetCore.ResponseCompression;
using Handyman_UI_Provider.Hubs;
using Handyman_UI_Provider.Hubs.Order;
using Handyman_UI_Provider.Hubs.Request;
using HandymanProviderLibrary.Api.Request;
using HandymanProviderLibrary.Api.ServiceProvider;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityDataContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityDataContextConnection' not found.");

builder.Services.AddDbContext<IdentityDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityDataContext>();



// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Dependency Injection
builder.Services.AddSingleton<IAPIHelper, APIHelper>();
builder.Services.AddTransient<IServiceEndpoint, ServiceEndpoint>();
builder.Services.AddSingleton<IRequestEndPoint,RequestEndPoint>();
builder.Services.AddTransient<IServiceProviderEndPoint, ServiceProviderEndPoint>();


builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});


//For identity token


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
//Mapping of Hub
app.MapHub<ChatHub>("/chathub");
app.MapHub<OrderHub>("/orderhub");
app.MapHub<RequestHub>("/requesthub");

app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();

app.Run();
