using Prism.Commands;
using Prism.Navigation;
using SignalRChat.Mobile.Helpers;
using SignalRChat.Mobile.Models;
using SignalRChat.Mobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Prism.Services;
using Xamarin.Forms;

namespace SignalRChat.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public string Password { get; set; }

        public DelegateCommand LoginCommand => new DelegateCommand(async () => await Login());

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base (navigationService)
        {
        }

        private async Task Login()
        {
            try
            {
                HubConnection = new HubConnectionBuilder()
                .WithUrl($"{BaseUrl}/chat", options =>
                {
                    var stringData = JsonConvert.SerializeObject(new { Email, Password });

                    options.AccessTokenProvider = async () =>
                    {
                        var content = new StringContent(stringData);
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var response = await HttpClientInstance.PostAsync($"{BaseUrl}/api/token", content);
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    };
                })
                .Build();

                UpdateConnectionState(ConnectionState.Connected);

                HubConnection.On<string, string>("newMessage", ReceiveMessage);

                await HubConnection.StartAsync();

                UpdateConnectionState(ConnectionState.Connected);
            }
            catch (Exception ex)
            {
                UpdateConnectionState(ConnectionState.Disconnected);
                await PageDialogService.DisplayAlertAsync("Connection Error", ex.Message, "OK", null);
            }
        }
    }
}
