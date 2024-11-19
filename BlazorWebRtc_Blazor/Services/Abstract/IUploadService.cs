using BlazorWebRtc_Blazor.Models.Response;
using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Blazor.Services.Abstract
{
    public interface IUploadService
    {
        Task<ResponseModel> UploadFileAsync(MultipartFormDataContent content);
    }
}
