using Blazored.LocalStorage;
using BlazorWebRtc_Blazor.Models;
using BlazorWebRtc_Blazor.Models.Response;
using BlazorWebRtc_Blazor.Services.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorWebRtc_Blazor.Services.Concrete
{
    public class UserInfoService : IUserInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        public UserInfoService(HttpClient httpClient,ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task<List<UserDTOResponseModel>> GetUserList()
        {
            //https://localhost:7282/api/UserInfo
            var token =await _localStorageService.GetItemAsync<string>(Constants.Local_Token);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            var response = await _httpClient.GetAsync("api/UserInfo");
            var content = await response.Content.ReadAsStringAsync();
            var deserialize = JsonConvert.DeserializeObject<ResponseModel>(content);
            if (response.IsSuccessStatusCode)
            {
                var desObj = JsonConvert.DeserializeObject<List<UserDTOResponseModel>>(deserialize.Data.ToString());
                return desObj;
            }
            return new List<UserDTOResponseModel>();
        }
    }
}
