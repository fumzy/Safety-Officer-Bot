using CustomerService.Model;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace CustomerService.Dialogs
{
    [LuisModel("102d4118-bf28-496b-9f01-3d96dab7f13b", "a8f0654fe7b446c2893e8eb3b68fe3cb", domain: "westus.api.cognitive.microsoft.com", Staging = true)]
    [Serializable]
    public class LUISDialog : LuisDialog<BugReport>
    {

        private BuildFormDelegate<BugReport> NewBugReport;
        private BuildFormDelegate<IncidenceReport> newincidentReport_;

        public LUISDialog(BuildFormDelegate<BugReport> newBugReport, BuildFormDelegate<IncidenceReport> newincidentReport)
        {
            this.NewBugReport = newBugReport;
            this.newincidentReport_ = newincidentReport;
        }



        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult luisResult)
        {
            await context.PostAsync("Sorry! I didnt get your request.");

            context.Wait(MessageReceived);
        }

        [LuisIntent("GreetingIntent")]
        public async Task Greeting(IDialogContext context, LuisResult luisResult)
        {
            context.Call(new GreetingDialog(), callback);
        }

        [LuisIntent("Hazard")]
        public async Task Hazard(IDialogContext context, LuisResult luisResult)
        {

            var HazardForm = new FormDialog<BugReport>(new BugReport(), this.NewBugReport, FormOptions.PromptInStart);
            context.Call<BugReport>(HazardForm, AfterGreetingContinuation);



        }

        [LuisIntent("IncidentIntent")]
        public async Task Incidence(IDialogContext context, LuisResult luisResult)
        {

            var IncidentForm = new FormDialog<IncidenceReport>(new IncidenceReport(), this.newincidentReport_, FormOptions.PromptInStart);
            context.Call<IncidenceReport>(IncidentForm, AfterGreetingContinuation);

        }


        private Task AfterGreetingContinuation(IDialogContext context, IAwaitable<object> result)
        {
           
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            //SaveReport();
            return context.PostAsync($"Thank you for reporting this, {name}");

        }

        private void SaveReport()
        {

            throw new NotImplementedException();
        }

        private async Task callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

    }
}