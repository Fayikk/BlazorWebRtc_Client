using Blazored.LocalStorage;
using Blazored.Toast;
using BlazorWebRtc_Blazor;
using BlazorWebRtc_Blazor.Extension;
using BlazorWebRtc_Blazor.Services.Abstract;
using BlazorWebRtc_Blazor.Services.Concrete;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUploadService,UploadService>(); 
builder.Services.AddScoped<IUserInfoService,UserInfoService>();
builder.Services.AddScoped<IRequestService,RequestService>();  
builder.Services.AddScoped<IUserFriendService,UserFriendService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IStorageService,StorageService>();
builder.Services.AddScoped<INotificationService,NotificationService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://192.168.1.190:7282/") });
builder.Services.AddScoped<AuthenticationStateProvider,CustomStateProvider>();
builder.Services.AddAuthorizationCore(); 
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp =>
{
    var hubConnection = new HubConnectionBuilder()
        .WithUrl("https://192.168.1.190:7282/userhub", options =>
        {
            options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
        })
        .Build();

    return hubConnection;
});


await builder.Build().RunAsync();
