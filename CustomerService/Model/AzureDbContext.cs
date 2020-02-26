using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CustomerService.Model
{
    public class AzureDbContext:DbContext
    {
        public AzureDbContext():base("ConversationDataContextConnectionString")
        {

        }
        public DbSet<ReportingActivities> HazardReporting { get; set; }
        
    }

}