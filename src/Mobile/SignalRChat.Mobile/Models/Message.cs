using System;
using System.Collections.Generic;
using System.Text;
using Humanizer;
using Prism.Mvvm;
using Prism.Navigation;
using SignalRChat.Mobile.ViewModels;

namespace SignalRChat.Mobile.Models
{
    public class Message : BindableBase
    {
        string text;

        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        DateTime messageDateTime;

        public DateTime MessageDateTime
        {
            get { return messageDateTime; }
            set { SetProperty(ref messageDateTime, value); }
        }

        public string MessageTimeDisplay => MessageDateTime.Humanize();

        bool isIncoming;

        public bool IsIncoming
        {
            get { return isIncoming; }
            set { SetProperty(ref isIncoming, value); }
        }

        string sender;
        public string Sender
        {
            get { return sender; }
            set { SetProperty(ref sender, value); }
        }
    }
}
