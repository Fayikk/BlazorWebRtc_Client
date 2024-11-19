using BlazorWebRtc_Application.Features.Commands.Account.Login;
using BlazorWebRtc_Application.Features.Commands.Account.Register;
using BlazorWebRtc_Application.Features.Commands.MessageCommand.SendMessage;
using BlazorWebRtc_Application.Features.Commands.RequestFeature;
using BlazorWebRtc_Application.Features.Commands.Upload;
using BlazorWebRtc_Application.Features.Commands.UserFriendFeature;
using BlazorWebRtc_Application.Features.Queries.MessageQuery;
using BlazorWebRtc_Application.Features.Queries.RequestFeature;
using BlazorWebRtc_Application.Features.Queries.UserFriend;
using BlazorWebRtc_Application.Features.Queries.UserInfo;
using BlazorWebRtc_Application.Hubs;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Interface.Services.Manager;
using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Application.Services;
using BlazorWebRtc_Application.Services.Manager;
using BlazorWebRtc_Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped(typeof(BaseResponseModel));
builder.Services.AddScoped<IAccountService, BlazorWebRtc_Application.Services.AccountService>();
builder.Services.AddScoped<IUserService,UserService>(); 
builder.Services.AddScoped<IRequestService,RequestService>();
builder.Services.AddScoped<IUserFriendService, UserFriendService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUploadService,UploadService>();
builder.Services.AddScoped<IConnectionManager, ConnectionManager>();
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(RegisterHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(LoginHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UserListHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RequestHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RequestsHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UserFriendHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UserFriendListQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(SendMessageHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UploadHandle).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetMessagesHandler).Assembly);

}
);
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
        ValidAudience = jwtSettings.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:5000",
                "https://localhost:5001",
                "http://192.168.1.190:5000",
                "https://192.168.1.190:5001"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});





builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
{
    {
       new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
    }
});
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseWebSockets();
app.UseCors("AllowBlazorApp");
app.MapHub<UserHub>("/userhub");
app.MapControllers();

app.Run();
