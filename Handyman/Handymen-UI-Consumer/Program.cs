using Microsoft.EntityFrameworkCore;
using Handymen_UI_Consumer.Data;
using Handymen_UI_Consumer.Areas.Identity.Data;
using HandymanUILibrary.API;
using HandymanUILibrary.API.Consumer;
using Microsoft.AspNetCore.ResponseCompression;
using Handymen_UI_Consumer.Helpers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Handymen_UI_ConsumerContextConnection") ?? throw new InvalidOperationException("Connection string 'Handymen_UI_ConsumerContextConnection' not found.");

builder.Services.AddDbContext<Handymen_UI_ConsumerContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Handymen_UI_ConsumerUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Handymen_UI_ConsumerContext>();



builder.Services.AddSingleton<IAPIHelper, APIHelper>();
builder.Services.AddTransient<IServiceEndPoint,ServiceEndPoint>();
builder.Services.AddTransient<IOrderEndPoint, OrderEndPoint>();
builder.Services.AddTransient<IOrderHelper, OrderHelper>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
});

builder.Services.AddResponseCompression(opt =>
{
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
      new[] { "application/octet-stream" });
});

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
app.MapControllers();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();
