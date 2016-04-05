using AirPortWebApi.App_Start;
using AirPortWebApi.BusinessLogic;
using AirPortWebApi.Common.Tools;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http;


namespace AirPortWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            config.MapHttpAttributeRoutes();
            
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            AutofacTypeRegister.RegisterTypes(builder);

            app.UseAutofacMiddleware(container);
            app.UseWebApi(config);
            app.UseAutofacWebApi(config);
            
            ConfigureAuth(app);

            // test
            app.Use((context, next) =>
            {
                TextWriter output = context.Get<TextWriter>("host.TraceOutput");
                return next().ContinueWith(result =>
                {
                    output.WriteLine("Scheme {0} : Method {1} : Path {2} : MS {3}",
                    context.Request.Scheme, context.Request.Method, context.Request.Path, getTime());
                });
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync(getTime() + " My  OWIN App");
            });

            //#test
        }

        string getTime()
        {
            return DateTime.Now.Millisecond.ToString();
        }
    }
}
