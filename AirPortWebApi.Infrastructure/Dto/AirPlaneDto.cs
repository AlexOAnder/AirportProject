using AirPortWebApi.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPortWebApi.Infrastructure.Dto
{
    public class AirPlaneDto : TechDto
    {
        public new TechType TechType { get { return TechType.AirPlane; } }
        public double MaxWeight { get; set; }
        public double MaxCapacity { get; set; }
    }
}
