namespace BlazorWebRtc_Blazor.Models.Response
{
    public class MessageListResponseModel
    {
        public DateTime CreateDate { get; set; }

        public string MessageContent { get; set; }
        public Guid? SenderUserId { get; set; }
        public Guid? ReceiverUserId { get; set; }
    }
}
