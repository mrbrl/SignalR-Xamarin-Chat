using Prism.Commands;
using Prism.Navigation;
using SignalRChat.Mobile.Helpers;
using SignalRChat.Mobile.Models;
using SignalRChat.Mobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;

namespace SignalRChat.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INavigationAware, INotifyPropertyChanged
    {
        private string _outgoingText = string.Empty;

        public ObservableRangeCollection<Message> Messages { get; }

        public string OutGoingText { get; set; }

        public DelegateCommand SendMessageCommand => new DelegateCommand(async () => await SendMessage());
        
        public MainPageViewModel() 
        {
            Title = "SignalR Chat";
            Messages = new ObservableRangeCollection<Message>();
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            HubConnection.On<string, string>("newMessage", DisplayMessage);

            await HubConnection.StartAsync();
        }

        private async Task SendMessage()
        {  
            var message = new Message
            {
                Text = OutGoingText,
                IsIncoming = false,
                MessageDateTime = DateTime.Now,
                Sender = Email,
                Icon = GravatarHelper.NetStandard.Gravatar.GetGravatarImageUrl(Email)
            };

            OutGoingText = string.Empty;

            await HubConnection.SendAsync("SendMessage", message.Text);
        }

        private void DisplayMessage(string sender, string text)
        {
            var message = new Message
            {
                Text = text,
                IsIncoming = sender != Email,
                MessageDateTime = DateTime.Now,
                Sender = sender,
                Icon = GravatarHelper.NetStandard.Gravatar.GetGravatarImageUrl(sender)
            };

            Messages.Add(message);
        }
    }
}