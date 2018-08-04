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
using SignalRChat.Mobile.Views;
using Xamarin.Forms;

namespace SignalRChat.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public string Username { get; set; }
        public string Password { get; set; }

        public DelegateCommand LoginCommand => new DelegateCommand(async () => await Login());

        public LoginPageViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
        }

        private async Task Login()
        {
            try
            {
                HubConnection = new HubConnectionBuilder()
                .WithUrl($"{Constants.BASE_URL}/chat", options =>
                {
                    var stringData = JsonConvert.SerializeObject(new { Username, Password });

                    options.AccessTokenProvider = async () =>
                    {
                        var content = new StringContent(stringData);
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var response = await HttpClientInstance.PostAsync($"{Constants.BASE_URL}/api/token", content);
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    };
                })
                .Build();

                Email = Username;

                await _navigationService.NavigateAsync(nameof(MainPage));
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("Connection Error", ex.Message, "OK", null);
            }
        }
    }
}
