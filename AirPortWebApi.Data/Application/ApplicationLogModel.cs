using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPortWebApi.Data.Application
{
    public class ApplicationLogModel
    {
        public int Id { get; set; }
        public DateTime ServerDateTime { get; set; }
        public Guid CorrelationId { get; set; }
        public string HttpMessageType { get; set; }
        public string RequestUri { get; set; }
        public string Method { get; set; }
        public int? StatusCode { get; set; }
        public string IpAddress { get; set; }
        public string UserName { get; set; }
        public int? AttendeeId { get; set; }
        public string RawData { get; set; }
    }
}
