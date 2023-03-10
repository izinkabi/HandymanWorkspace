using Handyman_Api.Areas.Identity.Data;
using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.DataAccess.Query;
using Handyman_DataLibrary.Internal.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using static Handyman_DataLibrary.DataAccess.Query.OrderData;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Handyman_DB") ?? throw new InvalidOperationException("Connection string 'IdentityDataContextConnection' not found.");

builder.Services.AddDbContext<IdentityDataContext>(options =>
    options.UseSqlServer(connectionString));


//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                .AddEntityFrameworkStores<IdentityDataContext>();

// Add services to the container
builder.Services.AddSingleton<ISQLDataAccess, SQLDataAccess>();
builder.Services.AddScoped<IServiceData, ServiceData>();
builder.Services.AddTransient<IOrderData, OrderData>();
builder.Services.AddScoped<EmployeeData>();
builder.Services.AddScoped<IServiceProviderData, ServiceProviderData>();
builder.Services.AddScoped<IBusinessData, BusinessData>();
builder.Services.AddTransient<IRequestData, RequestData>();
builder.Services.AddScoped<ITaskData, TaskData>();
builder.Services.AddScoped<IProfileData, ProfileData>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.DisableImplicitFromServicesParameters = true;
});
builder.Services.AddControllers(options =>
{
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
