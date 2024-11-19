using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebRtc_Application.DTO.Request
{
    public class GetRequestDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }    
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
