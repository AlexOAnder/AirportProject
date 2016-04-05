using AirPortWebApi.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPortWebApi.Infrastructure.Dto
{
    public class TechDto
    {
        public string ModelName { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public TechType TechType { get; set; }
        public DateTime StartExpluataionDate { get; set; }
    }
}
