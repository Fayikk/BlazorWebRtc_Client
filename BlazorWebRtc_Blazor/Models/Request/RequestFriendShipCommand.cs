namespace BlazorWebRtc_Blazor.Models.Request
{
    public enum Status
    {
        pending = 0,
        accept = 1,
        denied = 2,
    }

    public class RequestFriendShipCommand
    {
        public Status Status { get; set; } = Status.pending;
        public Guid ReceiverUserId { get; set; }
    }
}
