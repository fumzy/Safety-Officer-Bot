using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerService.Model
{
    public class ReportingActivities
    {
        public string Id { get; set; }
        public  string HazardCategory { get; set; }
        public string Preventive { get; set; }
        public string Corrective { get; set; }
        public String SafetyOfficer { get; set; }
        public string  StaffName { get; set; }
        public string Department { get; set; }
        public DateTime? DateOfObservation { get; set; }
        public DateTime? DateLogged { get; set; }
    }
}