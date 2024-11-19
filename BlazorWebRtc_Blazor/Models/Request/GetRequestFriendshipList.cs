namespace BlazorWebRtc_Blazor.Models.Request
{
    public class GetRequestFriendshipList
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
