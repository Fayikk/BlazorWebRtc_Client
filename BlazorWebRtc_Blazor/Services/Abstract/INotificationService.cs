using BlazorWebRtc_Blazor.Models.Request;
using BlazorWebRtc_Blazor.Models.Response;

namespace BlazorWebRtc_Blazor.Services.Abstract
{
    public interface INotificationService
    {
        Task<NotificationResponseModel> SendNotify(NotificationRequestModel requestModel);
    }
}
