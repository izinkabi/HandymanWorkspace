using System.Text;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("Handyman_DB") ?? throw new InvalidOperationException("Connection string 'IdentityDataContextConnection' not found.");

builder.Services.AddDbContext<IdentityDataContext>(options =>
    options.UseSqlServer(connectionString));

//Configure Identity to use User and Role identity and Framework as storage provider
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>();


// Add services to the container
//Dependency injection
builder.Services.AddSingleton<ISQLDataAccess, SQLDataAccess>();
builder.Services.AddScoped<IServiceData, ServiceData>();
builder.Services.AddTransient<IOrderData, OrderData>();
builder.Services.AddScoped<EmployeeData>();
builder.Services.AddScoped<IServiceProviderData, ServiceProviderData>();
builder.Services.AddScoped<IBusinessData, BusinessData>();
builder.Services.AddTransient<IRequestData, RequestData>();
builder.Services.AddScoped<ITaskData, TaskData>();
builder.Services.AddScoped<IProfileData, ProfileData>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Configure JWT authentication options
    options.SignIn.RequireConfirmedAccount = false; // Disable email confirmation for simplicity
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

//Configuration options for Authentication and adding a Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //We need to clean this, and get the secret Key from Configuration manager
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("JWT:JWTIssuer").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:JWTSecretKey").Value))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //Configure Swagger to Handle Authorizing using Bearer
    //Bearer token handling and API lock down
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        //Define the definition of the Authorize
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    //Add the requirement fot the Authorize. 
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "Bearer"
    //            }
    //        },
    //        Array.Empty<string>()
    //    }
    //});
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


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

app.UseCors("AllowAll");

app.UseResponseCaching();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
