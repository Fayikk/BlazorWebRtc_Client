﻿namespace BlazorWebRtc_Application.DTO
{
    public class UserDTO
    {
        public Guid UserId {  get; set; }   
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
