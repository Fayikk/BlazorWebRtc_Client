using BlazorWebRtc_Blazor.Models.Response;

namespace BlazorWebRtc_Blazor.Services.Abstract
{
    public interface IUserInfoService
    {
       
        Task<List<UserDTOResponseModel>> GetUserList();
    }
}
