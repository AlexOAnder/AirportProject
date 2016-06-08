using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirPortWebApi.Common.Tools;
using AirPortWebApi.Data.Application;
using AirPortWebApi.Data.DbContext;
using AirPortWebApi.Data.DbContext.Identity;
using AirPortWebApi.Data.DbContext.Models;
using AirPortWebApi.Infrastructure.Interfaces;
using AirPortWebApi.Infrastructure.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AirPortWebApi.Data
{
    public class Registrator: ModuleRegistrator
    {
        protected override void Init()
        {
            RegisterType<Entities>(Lifecycles.PerScope);
            RegisterType<ApplicationDbContext>(Lifecycles.PerScope);
            RegisterType<ApplicationLogsDbContext>(Lifecycles.PerScope);

        }
    }
}
