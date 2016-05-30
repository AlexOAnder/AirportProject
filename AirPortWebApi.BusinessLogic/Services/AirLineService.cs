using AirPortWebApi.Infrastructure.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirPortWebApi.BusinessLogic.Services
{
    public class AirLineService : IAirLineService
    {
        public AirLineService()
        { }

        public List<int> GetIdsOfActiveAirLine()
        {
            var tmp = new[] { 2, 3, 4, 5 };
            var list = tmp.ToList();
            return list;
        }
    }
}
