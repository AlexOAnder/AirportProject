
using System.Collections.Generic;
using Autofac;
using Autofac.Builder;

namespace AirPortWebApi.Common.Tools
{
    public static class AutoFacHelper
    {
        public static bool MatchingScopesOn => true;


        public static T Register<T>(ContainerBuilder builder) where T : ModuleRegistrator, new()
        {
            ModuleRegistrator moduleRegistrator = ModuleRegistrator.Create<T>();
            var registrationInfos = moduleRegistrator.Registrations;
            Register(builder, registrationInfos);
            return moduleRegistrator as T;
        }

        public static void Register(ContainerBuilder builder, IList<RegistrationInfo> registrationInfos)
        {
            foreach (var registrationInfo in registrationInfos)
            {
                IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> tempReg;
                tempReg = builder.RegisterType(registrationInfo.Implementation);
                foreach (var typeInterface in registrationInfo.Interfaces)
                {
                    if (registrationInfo.Key != null)
                    {
                        tempReg = tempReg.Keyed(registrationInfo.Key, typeInterface);
                    }
                    else
                    {
                        tempReg = tempReg.As(typeInterface);
                    }
                }

                if (registrationInfo.ScopeName != null)
                {
                    if (MatchingScopesOn)
                    {
                        tempReg.InstancePerMatchingLifetimeScope(registrationInfo.ScopeName);
                    }
                    else
                    {
                        tempReg.InstancePerLifetimeScope();
                    }
                }
                else
                {
                    switch (registrationInfo.Lifecycle)
                    {
                        case Lifecycles.PerDependency:
                            tempReg.InstancePerDependency();
                            break;
                        case Lifecycles.PerDefaultScope:
                            if (MatchingScopesOn)
                            {
                                //MainScope
                                tempReg.InstancePerMatchingLifetimeScope(0);
                            }
                            else
                            {
                                tempReg.InstancePerLifetimeScope();
                            }

                            break;
                        case Lifecycles.Singleton:
                            tempReg.SingleInstance();
                            break;
                        case Lifecycles.PerScope:
                            tempReg.InstancePerLifetimeScope();
                            break;
                    }
                }
            }
        }
    }
}

