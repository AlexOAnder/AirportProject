using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirPortWebApi.Infrastructure.Dto;

namespace AirPortWebApi.Infrastructure.Services
{
   public interface IStatusService
   {
       IEnumerable<string> GetStatuses();
       void Add(EventDto eventModel, UserDto currUser);
       IEnumerable<EventDto> GetAllLogs();
       void Update(EventDto eventModel, UserDto currUser);
       IEnumerable<SectionDto> GetSections();
   }
}
