using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirPortWebApi.BusinessLogic.Repositories;
using AirPortWebApi.Data.DbContext;
using AirPortWebApi.Infrastructure.Dto;
using AirPortWebApi.Infrastructure.Enum;
using AirPortWebApi.Infrastructure.Repositories;
using AirPortWebApi.Infrastructure.Services;

namespace AirPortWebApi.BusinessLogic.Services
{
    public class StatusService :IStatusService
    {
        private readonly IRepository<StatusType> _statusTypeRep;
        private readonly IRepository<EventLog> _eventLogRep;
        private readonly IRepository<SectionTable> _sectionTableRep; 

        public StatusService(IRepository<StatusType> statusTypeRep, IRepository<EventLog> eventLogRep, IRepository<SectionTable> sectionTableRep )
        {
            _statusTypeRep = statusTypeRep;
            _eventLogRep = eventLogRep;
            _sectionTableRep = sectionTableRep;
        }

        public IEnumerable<string> GetStatuses()
        {
            var ss = _statusTypeRep.Get();
            return ss.ToList().Select(x=>x.StatusName);
        }

        public IEnumerable<EventDto> GetAllLogs()
        {
            var arr = _eventLogRep.Get();
            var res = arr.Select(x => new EventDto()
            {
                EventId = x.Id,
                Description = x.Description,
                CreatedOn = x.Created,
                SectionId = x.SectionId,
                StatusId = x.StatusId,
                UpdatedOn = x.Updated,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            });
            return res;
        }

        public void Add(EventDto eventModel,UserDto currUser)
        {
            var answer = new EventLog()
            {
                Created =  DateTime.Now,
                CreatedBy = currUser.FirstName+currUser.LastName,
                StatusId = eventModel.StatusId,
                Description = eventModel.Description,
                SectionId = eventModel.SectionId
            };
            _eventLogRep.Add(answer);
            _eventLogRep.SaveChanges();
        }

        public void Update(EventDto eventModel,UserDto currUser)
        {
            // here we know that we have EventId
            
            var value = _eventLogRep.Get(x => x.Id == eventModel.EventId).FirstOrDefault();
            if (value==null) throw  new ValidationException("We dont have Event with Id"+eventModel.EventId);
            value.Updated = DateTime.Now;
            value.UpdatedBy = currUser.FirstName + currUser.LastName;
            value.StatusId = eventModel.EventId;
            value.Description = eventModel.Description;
            _eventLogRep.Update(value);
            _eventLogRep.SaveChanges();
        }

        public IEnumerable<SectionDto> GetSections()
        {
            var res = _sectionTableRep.Get();
            return res.Select(x => new SectionDto()
            {
                SectionId = x.Id,
                StatusId = -1,
                SectionName = x.SectionName,
                SectionDescription = x.Description
            });
        }
    }
}
