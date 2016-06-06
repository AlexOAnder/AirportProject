﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPortWebApi.Common.Tools
{
    public abstract class ModuleRegistrator
    {
        protected ModuleRegistrator()
        {
            Registrations = new List<RegistrationInfo>();
            RegistrationTypes = new List<RegistrationInfo>();
        }

        public List<RegistrationInfo> Registrations { get; private set; }
        public List<RegistrationInfo> RegistrationTypes { get; private set; }

        public static ModuleRegistrator Create<T>() where T : ModuleRegistrator, new()
        {
            var ret = new T();
            ret.Init();
            return ret;
        }

        protected abstract void Init();

        protected void RegisterType<TClass>(Lifecycles lifecycle = Lifecycles.PerDefaultScope) where TClass : class
        {
            RegistrationTypes.Add(new RegistrationInfo
            {
                Implementation = typeof(TClass),
                Interfaces = null,
                Lifecycle = lifecycle
            });
        }

        protected void RegisterType(Type type1, Type type2, Lifecycles lifecycle)
        {
            RegistrationTypes.Add(new RegistrationInfo
            {
                Implementation = type1,
                Interfaces = new[] { type2 },
                Lifecycle = lifecycle
            });
        }

        protected void RegisterType<TClass, TInterface>(Lifecycles lifecycle = Lifecycles.PerDefaultScope) where TClass : TInterface
        {
            RegistrationTypes.Add(new RegistrationInfo
            {
                Implementation = typeof(TClass),
                Interfaces = new[] { typeof(TInterface) },
                Lifecycle = lifecycle
            });
        }

        protected void Register<TClass, TInterface>(Lifecycles lifecycle = Lifecycles.PerDefaultScope) where TClass : TInterface
        {
            Registrations.Add(new RegistrationInfo
            {
                Implementation = typeof(TClass),
                Interfaces = new[] { typeof(TInterface) },
                Lifecycle = lifecycle
            });
        }

        protected void Register<TClass, TInterface1, TInterface2>(
            Lifecycles lifecycle = Lifecycles.PerDefaultScope) where TClass : TInterface1, TInterface2
        {
            Registrations.Add(new RegistrationInfo
            {
                Implementation = typeof(TClass),
                Interfaces = new[] { typeof(TInterface1), typeof(TInterface2) },
                Lifecycle = lifecycle
            });
        }

       
        protected void Register<TClass, TInterface>(object key, Lifecycles lifecycle = Lifecycles.PerDependency)
        {
            Registrations.Add(new RegistrationInfo
            {
                Implementation = typeof(TClass),
                Interfaces = new[] { typeof(TInterface) },
                Lifecycle = lifecycle,
                Key = key
            });
        }

        protected bool IsRegistered<TInterface>()
        {
            return
                Registrations.Any(
                    registrationInfo => Array.IndexOf(registrationInfo.Interfaces, typeof(TInterface)) > -1);
        }
    }
}
