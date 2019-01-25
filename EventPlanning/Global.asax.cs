using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EventPlanning.Util;
using Ninject;
using Ninject.Modules;
using EventPlanning.BLL.Infrastructure;
using Ninject.Web.Mvc;
using AutoMapper;

namespace EventPlanning
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule eventModule = new EventModule();
            NinjectModule serviceModule = new ServiceModule("EventContext");
            var kernel = new StandardKernel(eventModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            RegisterMappings();
        }

        private void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("EventPlanning")).ToArray();
                cfg.AddProfiles(assemblies);
                //cfg.AddProfiles(typeof(EventPlanning.Mapping.MappingProfile), typeof(EventPlanning.BLL.Infrastructure.MappingProfile));
            });
        }
    }
}
