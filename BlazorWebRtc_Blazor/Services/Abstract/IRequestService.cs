using BlazorWebRtc_Blazor.Models.Request;
using BlazorWebRtc_Blazor.Models.Response;

namespace BlazorWebRtc_Blazor.Services.Abstract
{
    public interface IRequestService
    {
        Task<ResponseModel> SendFriendshipRequest(RequestFriendShipCommand command);
       
        Task<List<GetRequestFriendshipList>> GetFriendshipRequest();

        Task<ResponseModel> UpdateRequest(UpdateRequestModel requestModel);
    }
}
