namespace BlazorWebRtc_Blazor.Models.Response
{
    public class UserDTOResponseModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public bool IsOnline { get; set; } = false;
    }
}
