using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Humanizer;
using Prism.Mvvm;
using Prism.Navigation;
using SignalRChat.Mobile.ViewModels;

namespace SignalRChat.Mobile.Models
{
    public class Message : BindableBase, INotifyPropertyChanged
    {
        public string Text { get; set; }

        public DateTime MessageDateTime { get; set; }

        public string MessageTimeDisplay => MessageDateTime.Humanize();
        
        public bool IsIncoming { get; set; }
        
        public string Sender { get; set; }

        public string Icon { get; set; }
    }
}
