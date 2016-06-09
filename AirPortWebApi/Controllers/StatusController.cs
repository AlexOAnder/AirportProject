using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using AirPortWebApi.BusinessLogic.Services;
using AirPortWebApi.Infrastructure.Dto;
using AirPortWebApi.Infrastructure.Service;
using AirPortWebApi.Infrastructure.Services;
using WebGrease.Css.Extensions;

namespace AirPortWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "get,post")]
    [RoutePrefix("api/mainstatus")]
    public class StatusController : ApiController
    {
        private readonly IStatusService _statusService;
        private readonly IRepairService _repairService;

        public StatusController(IStatusService statusService,IRepairService repairService)
        {
            _statusService = statusService;
            _repairService = repairService;
        }

        [HttpGet]
        [Route("check")]
        public HttpResponseMessage GetServerAvailabilityStatus()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("section")]
        public HttpResponseMessage GetSectionLastEvent([FromUri] int sectionId)
        {
            if (sectionId <=0) throw new ValidationException("Section Id cannot be less than 0");
            var logs = _statusService.GetAllLogs().ToList();
            var section = _statusService.GetSections().FirstOrDefault(x => x.SectionId == sectionId);
            if (section==null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Cannot find section with sended sectionId");
            var res = logs.FirstOrDefault(y => y.SectionId == section.SectionId);
            if (res == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No event available");
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllSystemStatus()
        {
            // main endpoint - return all status data about airport 
            // only status check - for the description we will use ither endpoints by request
            var logs = _statusService.GetAllLogs().ToList();
            var sections = _statusService.GetSections().OrderBy(x=>x.SectionId);

            var result = new List<SectionDto>();
            foreach (var section in sections)
            {
                section.Events = logs.Where(y => y.SectionId == section.SectionId).ToList();

                if (section.Events != null && section.Events.Any())
                {

                    var updatedDate = section.Events.Max(t => t.UpdatedOn);
                    var createdDate = section.Events.Max(t => t.CreatedOn);

                    var maxDate = updatedDate ?? createdDate;
                    if (updatedDate.HasValue)
                    {
                        var ss = section.Events.Where(y => y.UpdatedOn == maxDate).ToList();
                        section.StatusId = ss.First().StatusId;
                        section.Events = ss;
                    }
                    else
                    {
                        var ss = section.Events.Where(y => y.CreatedOn == maxDate).ToList();
                        section.StatusId = ss.First().StatusId;
                        section.Events = ss; // show only 1 event (last one)
                    }
                }
                result.Add(section);

            }
            
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }

        
        [HttpPost]
        [Route("addEvent")]
        public HttpResponseMessage AddEvent(EventDto eventModel)
        {
            var user = new UserDto(); 
            _statusService.Add(eventModel,user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("updateEvent")]
        public HttpResponseMessage UpdateEvent(EventDto eventModel)
        {
            var user = new UserDto();
            _statusService.Update(eventModel, user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}