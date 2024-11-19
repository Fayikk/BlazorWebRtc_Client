using Blazored.LocalStorage;
using BlazorWebRtc_Blazor.Extension;
using BlazorWebRtc_Blazor.Models;
using BlazorWebRtc_Blazor.Models.Request;
using BlazorWebRtc_Blazor.Models.Response;
using BlazorWebRtc_Blazor.Services.Abstract;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWebRtc_Blazor.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService; 
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AccountService(HttpClient httpClient,ILocalStorageService localStorageService,AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task<bool> Login(LoginCommand command)
        {
            var content = JsonConvert.SerializeObject(command);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/User/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);
            if (response.IsSuccessStatusCode)
            {
                await _localStorageService.SetItemAsync(Constants.Local_Token, result.Data.ToString());
                ((CustomStateProvider)_authenticationStateProvider).NotifyUserLoggedIn(result.Data.ToString());

                return result.isSuccess;
            }
            else
            {
                return false;
            }
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync(Constants.Local_Token);

            ((CustomStateProvider)_authenticationStateProvider).NotifyUserLogout();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<UserResponseModel> SignUp(RegisterCommand command)
        {
           var content = JsonConvert.SerializeObject(command);
           var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/User/register", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);
            var user = JsonConvert.DeserializeObject<UserResponseModel>(result.Data.ToString());
            if (response.IsSuccessStatusCode)
            {

                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
