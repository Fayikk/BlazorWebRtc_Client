using BlazorWebRtc_Blazor.Models.Request;
using BlazorWebRtc_Blazor.Models.Response;
using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Blazor.Services.Abstract
{

  

    public interface IAccountService
    {
        Task<UserResponseModel> SignUp(RegisterCommand command);

        Task<bool> Login(LoginCommand command);

        Task Logout();
    
    }
}
