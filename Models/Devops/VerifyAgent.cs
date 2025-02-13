using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Devops
{
    public class VerifyAgent
    {
        public Agent Agent { get; set; }
        public List<PingResult> PingResults { get; set; }
        public List<Auth> Auth { get; set; }
        public List<ServiceStatus> ServiceStatus { get; set; }
        public ServiceCheck ServiceCheck { get; set; }
        public IPsToPing IPsToPing { get; set; }
    }
}