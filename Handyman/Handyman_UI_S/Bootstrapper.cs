using System.Web.Mvc;
using Caliburn.Micro;
using Handyman_UI.Controllers;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using Unity;
using Unity.AspNet.Mvc;

namespace Handyman_UI_S
{
    public class Bootstrapper :BootstrapperBase
    {

        /// <summary>
        /// 
        /// </summary>
        public static void Initialise()
        {
          var  container = BuildUnityContainer();
           
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
           
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IloggedInUserModel, loggedInUserModel>();
            container.RegisterType<IAPIHelper, APIHelper>();
            container.RegisterType<IProfileEndPoint, ProfileEndPoint>();
            container.RegisterType<IRegisterEndPoint, RegisterEndPoint>();
            container.RegisterType<IServicesLoader, ServicesLoader>();
            container.RegisterType<IServiceEndPoint, ServiceEndPoint>();
            container.RegisterType<IServiceProviderEndPoint, ServiceProviderEndPoint>();
            container.RegisterType<IConsumerEndPoint, ConsumerEndPoint>();

            container.RegisterType<IRequestEndPoint, RequestEndPoint>();
           
            

            

            return container;
        }


    }
}