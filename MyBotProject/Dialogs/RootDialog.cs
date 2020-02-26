using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MyBotProject.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {

            var msg = await result as IMessageActivity;
            if (msg.Text.Equals(@"search", StringComparison.OrdinalIgnoreCase))
            {
                PromptDialog.Text(context, QueryEntered, @"Who do you want to search for?");
            }
            else if (msg.Text.StartsWith(@"search ", StringComparison.OrdinalIgnoreCase))
            {
                var query = msg.Text.Substring(7);
                await context.Forward<string, string>(new SearchDialog(), SearchComplete, query, default(CancellationToken));
            }
            //context.Call<UserProfile>(new EnsureProfileDialog(), ProfileEnsured);

            //return Task.CompletedTask;
            //var activity = await result as Activity;
            //PromptDialog.Text(context, NameEntered, @"Hi!, what's your name?");

            // Calculate something for us to return
            //int length = (activity.Text ?? string.Empty).Length;

            // Return our reply to the user
            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            //await context.PostAsync("Hello Dear");

            //return Task.CompletedTask;
        }

        private async Task QueryEntered(IDialogContext context, IAwaitable<string> result)
        {
            await context.Forward<string, string>(new SearchDialog(), SearchComplete, await result, default(CancellationToken));
        }



        private async Task SearchComplete(IDialogContext context, IAwaitable<string> result)
        {
            var msg = await result;

            var profile = await new GitHubClient().LoadProfile(await result);

            var thumbnail = new ThumbnailCard();
            thumbnail.Title = profile.Login;
            thumbnail.Images = new[] { new CardImage(profile.AvatarUrl) };

            if (!string.IsNullOrWhiteSpace(profile.Name)) thumbnail.Subtitle = profile.Name;

            string text = string.Empty;
            if (!string.IsNullOrWhiteSpace(profile.Company)) text += profile.Company + " \n";
            if (!string.IsNullOrWhiteSpace(profile.Email)) text += profile.Email + " \n";
            if (!string.IsNullOrWhiteSpace(profile.Bio)) text += profile.Bio;

            thumbnail.Text = text;

            thumbnail.Buttons = new[] { new CardAction(ActionTypes.OpenUrl, @"Click to view", value: profile.HtmlUrl) };

            var reply = context.MakeMessage();
            reply.Attachments.Add(thumbnail.ToAttachment());

            await context.PostAsync(reply);

            context.Wait(MessageReceivedAsync);
        }




        //private async Task ProfileEnsured(IDialogContext context, IAwaitable<UserProfile> result)
        //{
        //    var profile = await result;

        //    context.UserData.SetValue(@"profile", profile);

        //    await context.PostAsync($@"Hello {profile.Name}, I love {profile.Company}!");

        //    context.Wait(MessageReceivedAsync);
        //}

        //private async Task NameEntered(IDialogContext context, IAwaitable<string> result)
        //{
        //    await context.PostAsync($@"Hi {await result}!");
        //    context.Wait(MessageReceivedAsync);
        //}
    }
}