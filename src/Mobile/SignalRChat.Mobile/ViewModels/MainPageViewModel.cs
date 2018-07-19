using Prism.Commands;
using Prism.Navigation;
using SignalRChat.Mobile.Helpers;
using SignalRChat.Mobile.Models;
using SignalRChat.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;

namespace SignalRChat.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _outgoingText = string.Empty;

        public ObservableRangeCollection<Message> Messages { get; }

        public string OutGoingText
        {
            get => _outgoingText;
            set => SetProperty(ref _outgoingText, value);
        }

        public DelegateCommand SendMessageCommand => new DelegateCommand(async () => await SendMessage());
        
        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Block Chat";
            Messages = new ObservableRangeCollection<Message>();

            OnReceiveMessage = DisplayMessage;
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        private async Task SendMessage()
        {
            var message = new Message
            {
                Text = OutGoingText,
                IsIncoming = false,
                MessageDateTime = DateTime.Now,
                Sender = Email
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
                Sender = sender
            };

            Messages.Add(message);
        }
    }
}
