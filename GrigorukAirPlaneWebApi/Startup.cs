using Autofac;
using Autofac.Integration.WebApi;
using GrigorukAirPlaneWebApi;
using Microsoft.Owin;
using Owin;
using System.Web.Http;


[assembly: OwinStartup(typeof(Startup))]

namespace GrigorukAirPlaneWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
         /*   var builder = new ContainerBuilder();
            var container = builder.Build();
            app.UseAutofacMiddleware(container);
            ConfigureAuth(app);
            */
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();
            
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            ConfigureAuth(app);
        }
    }
}
