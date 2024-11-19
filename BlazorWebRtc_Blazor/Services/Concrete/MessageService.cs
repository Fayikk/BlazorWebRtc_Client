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
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public MessageService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<List<MessageListResponseModel>> GetMessageList(MessageQueryModel model)
        {
            var result = await((CustomStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();
            var data = JsonConvert.SerializeObject(model);

            var response = await _httpClient.GetAsync($"api/Message?userId={model.MessageUserId}");
            var content = await response.Content.ReadAsStringAsync();
            var deserialize = JsonConvert.DeserializeObject<ResponseModel>(content);
            if (deserialize.Data is null)
            {
                return new List<MessageListResponseModel>();
            }
            var desObj = JsonConvert.DeserializeObject<List<MessageListResponseModel>>(deserialize.Data.ToString());
            if (response.IsSuccessStatusCode)
            {
                return desObj;
            }
            return new List<MessageListResponseModel>();
        }

        public async Task<bool> SendMessage(SendMessageModel model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Message", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);
            if (response.IsSuccessStatusCode)
            {

                return result.isSuccess;
            }
            return false;
        }
    }
}
