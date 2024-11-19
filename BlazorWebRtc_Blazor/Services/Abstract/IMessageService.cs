using BlazorWebRtc_Blazor.Models.Request;
using BlazorWebRtc_Blazor.Models.Response;

namespace BlazorWebRtc_Blazor.Services.Abstract
{
    public interface IMessageService
    {

        Task<bool> SendMessage(SendMessageModel model);

        Task<List<MessageListResponseModel>> GetMessageList(MessageQueryModel model);
    }
}
