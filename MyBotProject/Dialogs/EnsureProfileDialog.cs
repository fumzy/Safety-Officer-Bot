﻿using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyBotProject
{
    [Serializable]
    public class EnsureProfileDialog : IDialog<UserProfile>
    {
        public Task StartAsync(IDialogContext context)
        {
            EnsureProfileName(context);

            return Task.CompletedTask;
        }

        UserProfile _profile;
        private void EnsureProfileName(IDialogContext context)
        {
            if (!context.UserData.TryGetValue(@"profile", out _profile))
            {
                _profile = new UserProfile();
            }

            if (string.IsNullOrWhiteSpace(_profile.Name))
            {
                PromptDialog.Text(context, NameEntered, @"What's your name?");
            }
            else
            {
                EnsureCompanyName(context);
            }
        }

        private void EnsureCompanyName(IDialogContext context)
        {
            if (string.IsNullOrWhiteSpace(_profile.Company))
            {
                PromptDialog.Text(context, CompanyEntered, @"Who do you work for?");
            }
            else
            {
                context.Done(_profile);
            }
        }

        private async Task CompanyEntered(IDialogContext context, IAwaitable<string> result)
        {
            _profile.Company = await result;

            context.Done(_profile);
        }

        private async Task NameEntered(IDialogContext context, IAwaitable<string> result)
        {
            _profile.Name = await result;

            EnsureCompanyName(context);
        }
    }
}