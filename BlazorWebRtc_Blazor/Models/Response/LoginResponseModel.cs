namespace BlazorWebRtc_Blazor.Models.Response
{
    public class LoginResponseModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
