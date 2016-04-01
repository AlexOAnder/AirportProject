
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Reflection;
using AirPortWebApi.Common.Tools;
using System.Web.Mvc;
using System.Web.Http;

namespace AirPortWebApi.App_Start
{
    public static class Boostrapper
    {
        private static ContainerBuilder Builder { get; set; }

        private static IContainer Container { get; set; }

        public static T GetObjectInstance<T>()
        {
            T ret;
            Container.TryResolve(out ret);
            return ret;
        }


        public static void Init()
        {
            Builder = new ContainerBuilder();
            RegisterTypes();
            Builder.RegisterControllers(Assembly.GetExecutingAssembly());
            Builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            Container = Builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }

        private static void RegisterTypes()
        {
            AutoFacHelper.Register<Infrastructure.Registrator>(Builder);
        }
    }
}