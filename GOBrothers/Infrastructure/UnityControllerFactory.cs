using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using GOBrothers.Domain.Abstract;
using GOBrothers.Domain.Concrete;
using GOBrothers.Models;
using System.Data.Entity;
using GOBrothers.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GOBrothers.Infrastructure
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer unityContainer;
        public UnityControllerFactory()
        {
            this.unityContainer = new UnityContainer();
            RegisterTypes();
            
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // получение объекта контроллера из контейнера 
            // используя его тип
            return controllerType == null
              ? null
              : (IController)this.unityContainer.Resolve(controllerType);
        }

        private void RegisterTypes()
        {
            this.unityContainer.RegisterType<IBloggingRepository, BloggingRepository>();
            this.unityContainer.RegisterType<IPortfolioRepository, PortfolioRepository>();
            this.unityContainer.RegisterType<IAlbumRepository, AlbumRepository>();

            // Registering types for working ASP.NET Identity with IoC
            this.unityContainer.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            this.unityContainer.RegisterType<UserManager<ApplicationUser>>();
            this.unityContainer.RegisterType<DbContext, ApplicationDbContext>();
            this.unityContainer.RegisterType<ApplicationUserManager>();
            this.unityContainer.RegisterType<AccountController>(new InjectionConstructor());
        }

    }
}