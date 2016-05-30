using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AirPortWebApi.Controllers
{
    [RoutePrefix("api/mainstatus")]
    public class StatusController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllSystemStatus()
        {
            // main endpoint - return all status data about airport 
            // only status check - for the description we will use ither endpoints by request
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}