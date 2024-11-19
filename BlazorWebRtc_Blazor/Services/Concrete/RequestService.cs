using Blazored.LocalStorage;
using BlazorWebRtc_Blazor.Extension;
using BlazorWebRtc_Blazor.Models.Request;
using BlazorWebRtc_Blazor.Models.Response;
using BlazorWebRtc_Blazor.Services.Abstract;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace BlazorWebRtc_Blazor.Services.Concrete
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public RequestService(HttpClient httpClient,AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
        }
        //https://localhost:7282/api/Request/38543a49-34d3-4481-b9e3-b309a7e993bf
        public async Task<List<GetRequestFriendshipList>> GetFriendshipRequest()
        {
            var result =await ((CustomStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();
            
            var response = await _httpClient.GetAsync($"api/Request/{result.User.FindFirst(ClaimTypes.NameIdentifier)?.Value}");
            var content = await response.Content.ReadAsStringAsync();
            var deserialize = JsonConvert.DeserializeObject<ResponseModel>(content);
            if (deserialize.Data is null)
            {
                return new List<GetRequestFriendshipList>();
            }
            var desObj = JsonConvert.DeserializeObject<List<GetRequestFriendshipList>>(deserialize.Data.ToString());
            if (response.IsSuccessStatusCode)
            {
                return desObj;
            }
            return new List<GetRequestFriendshipList>();
        }

        public async Task<ResponseModel> SendFriendshipRequest(RequestFriendShipCommand command)
        {
            var content = JsonConvert.SerializeObject(command);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Request", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);
            if (response.IsSuccessStatusCode)
            {

                return result;
            }
            return new ResponseModel { isSuccess = false };
        }

        public async Task<ResponseModel> UpdateRequest(UpdateRequestModel requestModel)
        {

            var content = JsonConvert.SerializeObject(requestModel);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/Request", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);
            if (response.IsSuccessStatusCode)
            {

                return result;
            }
            return new ResponseModel { isSuccess = false };
        }
    }
}
