using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Application.Models
{
    public class UploadFileModel
    {
        public IFormFile File {  get; set; }    
        public string UserId { get; set; }  
    }
}
