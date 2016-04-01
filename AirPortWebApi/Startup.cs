using AirPortWebApi;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace AirPortWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
         /*   var builder = new ContainerBuilder();
            var config = new HttpConfiguration();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacWebApi(config);*/
            ConfigureAuth(app);
        }
    }
}
