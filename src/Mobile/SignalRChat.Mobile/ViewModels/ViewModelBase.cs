using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Services;

namespace SignalRChat.Mobile.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected static readonly HttpClient HttpClientInstance = new HttpClient();
        protected const string BaseUrl = "http://localhost:3528";
        protected HubConnection HubConnection;
        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService PageDialogService { get; private set; }

        public ConnectionState ConnectionStatus { get; set; }

        public Action<string, string> OnReceiveMessage;

        public string Email { get; set; }


        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        public virtual void Destroy()
        {
            
        }

        protected void UpdateConnectionState(ConnectionState status)
        {
            if (ConnectionStatus == status)
            {
                return;
            }
           
            ConnectionStatus = status;
        }

        protected void ReceiveMessage(string sender, string message)
        {
            OnReceiveMessage(sender, message);
        }

        public enum ConnectionState
        {
            Disconnected,
            Connecting,
            Connected,
            Disconnecting
        }


    }
}
