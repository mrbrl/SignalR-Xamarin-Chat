using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Services;

namespace SignalRChat.Mobile.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, INotifyPropertyChanged
    {
        protected static readonly HttpClient HttpClientInstance = new HttpClient();
        protected static HubConnection HubConnection;
        protected IPageDialogService PageDialogService { get; private set; }
        
        public static string Email { get; set; }

        protected string Title { get; set; }

        public ViewModelBase()
        {
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

        public void Destroy()
        {
           
        }
    }
}
