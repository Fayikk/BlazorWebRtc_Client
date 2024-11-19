namespace BlazorWebRtc_Application.DTO.Message
{
    public class MessageDTO
    {
        public DateTime CreateDate { get; set; }

        public string MessageContent { get; set; }
        public Guid? SenderUserId { get; set; }
        public Guid? ReceiverUserId { get; set; }
    }
}
