
using AirPortWebApi.BusinessLogic.Service;
using AirPortWebApi.Common.Tools;
using AirPortWebApi.Infrastructure.Service;

namespace AirPortWebApi.BusinessLogic
{
    public class Registrator : ModuleRegistrator
    {
        protected override void Init()
        {
            Register<AirLineService, IAirLineService>(Lifecycles.PerScope);
            Register<PersonalService, IPersonalService>(Lifecycles.PerScope);
            Register<RepairService, IRepairService>(Lifecycles.PerScope);
            Register<TechService, ITechService>(Lifecycles.PerScope);
        }

    }
}
