using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AirPortWebApi.BusinessLogic.Services;
using AirPortWebApi.Infrastructure.Dto;
using AirPortWebApi.Infrastructure.Service;
using AirPortWebApi.Infrastructure.Services;

namespace AirPortWebApi.Controllers
{
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
        [Route("")]
        public HttpResponseMessage GetAllSystemStatus()
        {
            // main endpoint - return all status data about airport 
            // only status check - for the description we will use ither endpoints by request
            var res = _statusService.Get();
            var items = new List<SectionDto>();
            var events = new List<EventDto>();

            return Request.CreateResponse(HttpStatusCode.OK,res);
        }

    }
}