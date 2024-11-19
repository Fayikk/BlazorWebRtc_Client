using BlazorWebRtc_Application.DTO.Message;
using MediatR;

namespace BlazorWebRtc_Application.Features.Queries.MessageQuery
{
    public class GetMessagesQuery : IRequest<List<MessageDTO>>
    {
        public string MessageUserId { get; set; }    
    }
}
