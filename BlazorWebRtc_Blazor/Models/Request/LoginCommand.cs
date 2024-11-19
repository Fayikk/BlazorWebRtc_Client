using System.ComponentModel.DataAnnotations;

namespace BlazorWebRtc_Blazor.Models.Request
{
    public class LoginCommand
    {
        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
