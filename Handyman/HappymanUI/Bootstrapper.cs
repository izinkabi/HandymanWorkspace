using System;
using AutoMapper;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;

namespace HappymanUI
{
   
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IloggedInUserModel, loggedInUserModel>()
                .Singleton<IAPIHelper, APIHelper>();
        }


            ////This is where we specify how we select our Types of Instances for views in the shell
            //GetType().Assembly.GetTypes()
            //    .Where(type => type.IsClass)
            //    .Where(type => type.Name.EndsWith("ViewModel"))
            //    .ToList()
            //    .ForEach(viewModelType => _container.RegisterPerRequest(
            //        viewModelType, viewModelType.ToString(), viewModelType));

         protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}