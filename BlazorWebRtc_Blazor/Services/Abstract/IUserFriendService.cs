using BlazorWebRtc_Blazor.Models.Response;

namespace BlazorWebRtc_Blazor.Services.Abstract
{
    public interface IUserFriendService
    {
        //https://localhost:7282/api/UserFriend
        Task<List<UserDTOResponseModel>> GetAllFriendsByUser();
    }
}
