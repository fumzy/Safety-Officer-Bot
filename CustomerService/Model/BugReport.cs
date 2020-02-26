using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerService.Model
{
    [Serializable]
    public class BugReport
    {
       
        

        public enum Hazards
        {
            trailingcables=1,
            wetfloor=2,
            poorlightning=3,
            obstructions=4,
            noise=5,
            others=6,
            NotApplicable = 7


        }
        
        

        public List<Hazards> hazard { get; set; }
        
        [Prompt("When did you first notice this hazard?")]
        public DateTime? Time { get; set; }
        //[Pattern(@"^[0]\d{10}$")]
        //public string PhoneNumber { get; set; }
       
        [Prompt("What preventive measure should be taken?")]
        public string Preventive { get; set; }
        [Prompt("What corrective action should be taken?")]
        public string Corrective { get; set; }
        [Prompt("Please enter the staffID of your safety officer")]
        public String SafetyOfficer { get; set; }

        //[Prompt("Please select a Hazard")]


        //public static IForm<BugReport> BuildForm()
        //{

        //    return new FormBuilder<BugReport>().Message("You need to fill a hazard Spotting form").Build();

        //}


        public static IForm<BugReport> BuildForm()
        {
           
            return new FormBuilder<BugReport>()
                    .Message("You need to fill a hazard Spotting form")
                        .OnCompletion(async (context, HardwareQueryForm) =>
                        {
                            //you code logic 

                            //OHSASBOTEntities1 DB = new OHSASBOTEntities1();
                            // Create a new UserLog object
                            ReportingActivities NewUserLog = new ReportingActivities();
                            // Set the properties on the UserLog object
                          
                            NewUserLog.Corrective = HardwareQueryForm.Corrective;
                            NewUserLog.Preventive = HardwareQueryForm.Preventive;
                            NewUserLog.HazardCategory = HardwareQueryForm.hazard.ToString();
                            NewUserLog.SafetyOfficer = HardwareQueryForm.SafetyOfficer;
                            NewUserLog.DateOfObservation = HardwareQueryForm.Time;
                            NewUserLog.StaffName = "Admin";
                            NewUserLog.Department = "Admin Department";
                            NewUserLog.DateLogged = DateTime.UtcNow;

                            //NewUserLog = activity.Text.Truncate(500);
                            // Add the UserLog object to UserLogs
                            //DB.ReportingActivities.Add(NewUserLog);
                            // Save the changes to the database
                            //DB.SaveChanges();

                          

                    //connect to database and save user inputs into database
                })
                        .Build();
        }



    }

   
}