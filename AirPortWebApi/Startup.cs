
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using AirPortWebApi.BusinessLogic.Services;
using AirPortWebApi.Infrastructure.Service;
using AirPortWebApi.Infrastructure.Services;
using Microsoft.AspNet.Identity.Owin;


namespace AirPortWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();

            var corsAttr = new EnableCorsAttribute("http://localhost:3000", "*", "*");
            config.EnableCors(corsAttr);

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            AutofacTypeRegister.RegisterTypes(builder);
            
            var container = builder.Build();
            
            
           
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);

            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
            
            
            ConfigureAuth(app);

            
        }

        string getTime()
        {
            return DateTime.Now.Millisecond.ToString();
        }
    }
}
