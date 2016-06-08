using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPortWebApi.Infrastructure.Enum
{
    public enum StatusEnum
    {
        OK = 1,
        Info = 2, // light
        Warning = 3, //medium
        Danger = 4 // Epic
    }
}
