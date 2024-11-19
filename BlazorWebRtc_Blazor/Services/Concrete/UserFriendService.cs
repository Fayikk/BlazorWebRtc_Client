using Blazored.LocalStorage;
using BlazorWebRtc_Blazor.Models.Response;
using BlazorWebRtc_Blazor.Services.Abstract;
using Newtonsoft.Json;
using System.Net.Http;

namespace BlazorWebRtc_Blazor.Services.Concrete
{
    public class UserFriendService : IUserFriendService
    {
        private readonly HttpClient httpClient;
        public UserFriendService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<UserDTOResponseModel>> GetAllFriendsByUser()
        {
            //var token = await _localStorageService.GetItemAsync<string>(Constants.Local_Token);
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            var response = await httpClient.GetAsync("api/UserFriend");
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
