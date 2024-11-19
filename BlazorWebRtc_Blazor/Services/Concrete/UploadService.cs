using BlazorWebRtc_Blazor.Models.Response;
using BlazorWebRtc_Blazor.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace BlazorWebRtc_Blazor.Services.Concrete
{
    public class UploadService : IUploadService
    {
        private readonly HttpClient _httpClient;

        public UploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseModel> UploadFileAsync(MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync("api/upload",content);
            if (response.IsSuccessStatusCode) { 
                var result = await response.Content.ReadFromJsonAsync<ResponseModel>();
                return result;
            }

            return new ResponseModel { isSuccess = false ,Message = "File upload failed"};
        }
    }
}
