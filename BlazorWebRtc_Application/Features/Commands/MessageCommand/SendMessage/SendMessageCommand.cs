using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.MessageCommand.SendMessage
{
    public class SendMessageCommand : IRequest<bool>
    {
        public string MessageContent { get; set; }
        public Guid ReceiverUserId { get; set; }        
    }
}
