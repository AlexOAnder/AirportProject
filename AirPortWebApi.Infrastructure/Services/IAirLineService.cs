﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPortWebApi.Infrastructure.Service
{
    public interface IAirLineService
    {
        List<int> GetIdsOfActiveAirLine();
    }
}
