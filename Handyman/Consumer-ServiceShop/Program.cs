global using Microsoft.AspNetCore.Components.Authorization;
using Consumer_ServiceShop;
using Consumer_ServiceShop.Helpers;
using Consumer_ServiceShop.Helpers.Tasks;
using HandymanUILibrary.API.Auth;
using HandymanUILibrary.API.Consumer.Order.Implementation;
using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.API.Services;
using HandymanUILibrary.API.User;
using HandymanUILibrary.Models;
using HandymanUILibrary.Models.Auth;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IAPIHelper, APIHelper>();
builder.Services.AddTransient<IServiceEndPoint, ServiceEndPoint>();
builder.Services.AddTransient<IOrderEndPoint, OrderEndPoint>();
builder.Services.AddScoped<IOrderHelper, OrderHelper>();
builder.Services.AddTransient<ITasksEndPoint, TasksEndPoint>();
builder.Services.AddTransient<ITasksHelper, TasksHelper>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IAuthEndpoint, AuthEndpoint>();
builder.Services.AddScoped<AuthenticatedUserModel>();
builder.Services.AddScoped<Settings>();


builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
