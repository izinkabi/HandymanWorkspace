using System.Web.Mvc;
using Caliburn.Micro;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace Handyman_UI
{
    public static class Bootstrapper
    {

        
        public static void Initialise()
        {
          var  container = BuildUnityContainer();
           
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
           
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IloggedInUserModel, loggedInUserModel>();
            container.RegisterType<IAPIHelper, APIHelper>();
            container.RegisterType<IProfileEndPoint, ProfileEndPoint>();
            container.RegisterType<IRegisterEndPoint, RegisterEndPoint>();
            container.RegisterType<IRegisterProviderEndPoint, RegisterProviderEndPoint>();


            // e.g. container.RegisterType<ITestService, TestService>();            

            return container;
        }


    }
}