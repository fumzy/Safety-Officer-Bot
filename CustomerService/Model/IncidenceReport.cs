using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerService.Model
{
    [Serializable]
    public class IncidenceReport
    {

        public enum Incidents
        {
            Minor = 1,
            Critical = 2,
            Fatal = 3,
            Nearmiss = 4

        }



        public Incidents Incident { get; set; }
        [Prompt("what is the name  of the victim?")]
        public string Victim { get; set; }
        [Prompt("What did this happen?")]
        public DateTime? Time { get; set; }
        [Prompt("Please give a brief narration of what happened")]
        public string Narration { get; set; }
        [Prompt("what part(s) of the victim's body was injured?")]
        public string InjuryLocation { get; set; }
        [Prompt("What preventive action do you recommend?")]
        public string Preventive { get; set; }
        [Prompt("What corrective measure was taken?")]
        public string Corrective { get; set; }
        [Prompt("Please enter the staffID of your safety officer")]
        public String SafetyOfficer { get; set; }
        //[Pattern(@"^[0]\d{10}$")]
        //public string PhoneNumber { get; set; }

        public static IForm<IncidenceReport> BuildForm()
        {
            return new FormBuilder<IncidenceReport>().Message("You need to fill an Incident Investigation Report").Build();

        }


    }
}