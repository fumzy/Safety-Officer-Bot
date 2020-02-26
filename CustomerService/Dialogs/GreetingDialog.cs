using CustomerService.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace CustomerService.Dialogs
{
    [Serializable]

    public class GreetingDialog : IDialog<object>
    {
    
      
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi! I am a Safety Officer Bot");
            await Respond(context);
            context.Wait(MessageReceivedAsync);

        }

        private static async Task Respond(IDialogContext context)
        {
            var userName = string.Empty;
            context.UserData.TryGetValue<string>("Name", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("Please enter your staff Id");
                
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
               
                await context.PostAsync(string.Format("Hi, {0}, how may I help you today?", userName));

            }

        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            
            var message = await result;
            var userName = string.Empty;
            var getName = false;
            var option = message.Text;

            context.UserData.TryGetValue<string>("Name", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);
            //string url = ConfigurationSettings.AppSettings["BANKAPIUrl"].ToString();
            string url = "http://192.168.41.126/bankAPI/humanresources/v1/employees?employee_id={0}";
            string completeUri = String.Format(url, option);
            HttpWebRequest req = HttpWebRequest.Create(completeUri) as HttpWebRequest;


            var resp = req.GetResponse();
            Stream receiveStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
            string content = reader.ReadToEnd();

            StaffDetailsApi deserializedProduct = JsonConvert.DeserializeObject<StaffDetailsApi>(content);
            



            if (deserializedProduct != null)
            {
                if (getName)
                {
                    userName = deserializedProduct.staff_name;
                    context.UserData.SetValue<string>("Name", userName);
                    context.UserData.SetValue<bool>("GetName", false);
                    await Respond(context);
                    context.Wait(MessageReceivedAsync);
                }


                else
                {
                    context.Done(message);
                }
                       
            }
            else
            {
                await context.PostAsync("Sorry! I cant identify you as a staff of Fidelitybank plc!");
            }
          

        }


       


    }
}