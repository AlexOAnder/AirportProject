using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPortWebApi.Common.Tools
{
    public enum Lifecycles
    {
        Singleton,

        PerDefaultScope,

        PerDependency,

        PerScope
    }
}
