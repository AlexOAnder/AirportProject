using System;

namespace AirPortWebApi.Common.Tools
{
    public class RegistrationInfo
    {
        public Type Implementation { get; set; }

        public Type[] Interfaces { get; set; }

        public object Key { get; set; }

        public Lifecycles Lifecycle { get; set; }

        public string ScopeName { get; set; }
    }
}
