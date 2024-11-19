using BlazorWebRtc_Blazor.Extension;
using BlazorWebRtc_Blazor.Services.Abstract;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorWebRtc_Blazor.Services.Concrete
{
    public class StorageService : IStorageService
    {
        private readonly AuthenticationStateProvider stateProvider;
        public StorageService(AuthenticationStateProvider stateProvider)
        {
            this.stateProvider = stateProvider;
        }

        public string GetUserId()
        {
            var result = ((CustomStateProvider)stateProvider).GetAuthenticationStateAsync().Result;
            return result.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
