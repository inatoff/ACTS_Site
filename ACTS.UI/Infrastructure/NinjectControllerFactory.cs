using Ninject;
using ACTS.Core.Concrete;
using ACTS.Core.Abstract;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace ACTS.UI.Infrastructure
{   // реализация пользовательской фабрики контроллеров,    
    // наследуясь от фабрики используемой по умолчанию   
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            // создание контейнера   
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // получение объекта контроллера из контейнера        
            // используя его тип       
            return controllerType == null
             ? null
             : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {       // конфигурирование контейнера  
            ninjectKernel.Bind<IEventRepository>().To<EFEventRepository>();
           // ninjectKernel.Bind<IEmployeeRepository>().To<EFEmployeeRepository>();
            ninjectKernel.Bind<INewsRepository>().To<EFNewsRepository>();
        }
    }
}