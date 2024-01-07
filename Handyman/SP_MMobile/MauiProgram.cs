using Microsoft.Extensions.Logging;
using SP_MLibrary.Models;
using SP_MLibrary.Services.Implementation;
using SP_MLibrary.Services.Interface;
using SP_MLibrary.Services.ServiceHelper;
using SP_MMobile.ViewModels;
using SP_MMobile.Views;

namespace SP_MMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IServiceHelper, ServicesHelper>();

            builder.Services.AddTransient<IWorkshopEndPoint, WorkshopEndPoint>();
            builder.Services.AddTransient<AuthenticatedUserModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<NewOrdersPage>();
            builder.Services.AddTransient<NewOrdersViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<NewOrdersViewModel>();
            builder.Services.AddTransient<IOrderEndpoint, OrderEndpoint>();
            builder.Services.AddTransient<IAuthEndpoint, AuthEndpoint>();
            builder.Services.AddTransient<OrderDetailsPage>();
            builder.Services.AddTransient<OrderDetailsViewModel>();

            //var a = Assembly.GetExecutingAssembly();
            //using var stream = a.GetManifestResourceStream("SP_MMobile.appsettings.json");

            //var config = new ConfigurationBuilder()
            //            .AddJsonStream(stream)
            //            .Build();


            //builder.Configuration.AddConfiguration(config);



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
