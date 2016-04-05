
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Reflection;
using AirPortWebApi.Common.Tools;
using System.Web.Mvc;
using System.Web.Http;
using AirPortWebApi.BusinessLogic;

namespace AirPortWebApi.App_Start
{
    public static class AutofacTypeRegister
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            AutoFacHelper.Register<Registrator>(builder);
        }
    }
}