using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Handymen_UI_Consumer.Data;
using Handymen_UI_Consumer.Areas.Identity.Data;
using HandymanUILibrary.API;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Handymen_UI_ConsumerContextConnection") ?? throw new InvalidOperationException("Connection string 'Handymen_UI_ConsumerContextConnection' not found.");

builder.Services.AddDbContext<Handymen_UI_ConsumerContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Handymen_UI_ConsumerUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Handymen_UI_ConsumerContext>();

builder.Services.AddSingleton<IAPIHelper, APIHelper>();
builder.Services.AddTransient<IServiceEndPoint,ServiceEndPoint>();

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

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
