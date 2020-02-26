using CustomerService.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CustomerService.Dialogs
{
    [LuisModel("102d4118-bf28-496b-9f01-3d96dab7f13b", "a8f0654fe7b446c2893e8eb3b68fe3cb", domain: "westus.api.cognitive.microsoft.com", Staging = true)]
    [Serializable]

    public class IncidentDialog : IDialog<object>
    {



        public async Task StartAsync(IDialogContext context)
        {
            
        }


        private BuildFormDelegate<IncidenceReport> newIncidentReport_;


        public IncidentDialog(BuildFormDelegate<IncidenceReport> newIncidentReport)
        {
            this.newIncidentReport_ = newIncidentReport;
        }

        public virtual async Task Incidence(IDialogContext context, LuisResult luisResult)
        {
            var IncidentForm = new FormDialog<IncidenceReport>(new IncidenceReport(), this.newIncidentReport_, FormOptions.PromptInStart);
            context.Call<IncidenceReport>(IncidentForm, AfterGreetingContinuation);
        }

        private Task AfterGreetingContinuation(IDialogContext context, IAwaitable<IncidenceReport> result)
        {
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            return context.PostAsync($"Thank you for reporting this Incidence, {name}");
        }
    }
    }
    

    






