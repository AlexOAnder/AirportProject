using System;
using AirPortWebApi.BusinessLogic.Repositories;
using AirPortWebApi.BusinessLogic.Services;
using AirPortWebApi.Common.Tools;
using AirPortWebApi.Infrastructure.Interfaces;
using AirPortWebApi.Infrastructure.Repositories;
using AirPortWebApi.Infrastructure.Service;
using AirPortWebApi.Infrastructure.Services;

namespace AirPortWebApi.BusinessLogic
{
    public class Registrator : ModuleRegistrator
    {
        protected override void Init()
        {
             
            Register<AirLineService, IAirLineService>(Lifecycles.PerScope);
            Register<PersonalService, IPersonalService>(Lifecycles.PerScope);
            Register<StatusService, IStatusService>(Lifecycles.PerScope);
            Register<RepairService, IRepairService>(Lifecycles.PerScope);
            Register<TechService, ITechService>(Lifecycles.PerScope);

            Register<DbContextContainer, IDbContextContainer>(Lifecycles.PerScope);
            RegisterType(typeof(Repository<>),typeof(IRepository<>),Lifecycles.PerScope);

            

        }

      
    }
}
