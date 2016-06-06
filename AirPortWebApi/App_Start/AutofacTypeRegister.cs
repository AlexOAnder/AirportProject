using AirPortWebApi.BusinessLogic;
using AirPortWebApi.Common.Tools;
using Autofac;

namespace AirPortWebApi
{
    public static class AutofacTypeRegister
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            AutoFacHelper.Register<Data.Registrator>(builder);
            AutoFacHelper.Register<Registrator>(builder);
            
        }
    }
}