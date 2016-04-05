using AirPortWebApi.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPortWebApi.Infrastructure.Dto
{
    public class SupportTechDto :TechDto
    {
        public new TechType TechType { get { return TechType.Support; } } 
        public string DeportamentNumber { get; set; }
    }
}
