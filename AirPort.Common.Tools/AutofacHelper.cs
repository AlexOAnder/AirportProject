
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            var regTypes = moduleRegistrator.RegistrationTypes;
            Register(builder, registrationInfos);
            RegisterTypes(builder, regTypes);
            return moduleRegistrator as T;
        }
        // register types block
        public static void RegisterTypes(ContainerBuilder builder, IList<RegistrationInfo> types)
        {
            foreach (var item in types)
            {
                if (item.Interfaces != null && item.Interfaces.Length>0)
                {
                    RegisterType(builder, item.Implementation, item.Interfaces.First(), item.Lifecycle);
                }
                else
                {
                    RegisterType(builder, item.Implementation, item.Lifecycle);
                }

            }
        }

        public static void RegisterType(ContainerBuilder builder,Type regType,Lifecycles lifecycle) 
        {
            var bld = builder.RegisterType(regType);
            switch (lifecycle)
            {
                case Lifecycles.Singleton:
                    bld.SingleInstance();
                    break;
                case Lifecycles.PerScope:
                    bld.InstancePerLifetimeScope();
                    break;
                case Lifecycles.PerDependency:
                    bld.InstancePerDependency();
                    break;
            }
        }

        public static void RegisterType(ContainerBuilder builder, Type regType, Type interfaceType, Lifecycles lifecycle)
        {
            if (interfaceType.IsGenericType)
            {
                var bld = builder.RegisterGeneric(regType).As(interfaceType);
            
                switch (lifecycle)
                {
                    case Lifecycles.Singleton:
                        bld.SingleInstance();
                        break;
                    case Lifecycles.PerScope:
                        bld.InstancePerLifetimeScope();
                        break;
                    case Lifecycles.PerDependency:
                        bld.InstancePerDependency();
                        break;
                }
            }
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

