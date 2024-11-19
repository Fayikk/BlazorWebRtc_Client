using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlazorWebRtc_Blazor.Models.Request
{
    public class RegisterCommand
    {
        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]

        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Is Required")]

        public string ConfirmPassword { get; set; }

        public IFormFile? ProfilePicture { get; set; }
    }

}
