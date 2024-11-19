using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Model.Models.Account
{
    public class RegisterCommand
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}
