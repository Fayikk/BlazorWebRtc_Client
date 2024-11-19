using BlazorWebRtc_Application.DTO.Message;
using BlazorWebRtc_Application.Features.Commands.MessageCommand.SendMessage;
using BlazorWebRtc_Application.Features.Queries.MessageQuery;
using BlazorWebRtc_Application.Hubs;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Interface.Services.Manager;
using BlazorWebRtc_Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMediator mediator;
        private readonly BaseResponseModel _responseModel;
        private IHubContext<UserHub> hubContext;
        private readonly IConnectionManager connectionManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public MessageService(IMediator mediator,IHttpContextAccessor httpContextAccessor,IConnectionManager connectionManager,IHubContext<UserHub> hubContext, BaseResponseModel responseModel)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.connectionManager = connectionManager;
            this.hubContext = hubContext;
            this.mediator = mediator;
            _responseModel = responseModel;
        }

        public async Task<BaseResponseModel> GetListMessage(GetMessagesQuery query)
        {
            var response = await mediator.Send(query);
            if (response.Count > 0)
            {
                _responseModel.isSuccess = true;
                _responseModel.Data = response;
                return _responseModel;
            }

            _responseModel.isSuccess = false;
            return _responseModel;
        }

        public async Task<BaseResponseModel> SendMessage(SendMessageCommand command)
        {
            var response = await mediator.Send(command);
            if (response != null)
            {

                List<MessageDTO> messages = new();

                var senderUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                List<string> _userIds = new List<string>() { senderUserId, command.ReceiverUserId.ToString() };


                var userIds = connectionManager.GetConnectionByUserId(_userIds);
                GetMessagesQuery query = new();
                query.MessageUserId = command.ReceiverUserId.ToString();
                var obj = await mediator.Send(query);
                var serializeMessages = JsonConvert.SerializeObject(obj);

                await hubContext.Clients.Clients(userIds).SendAsync("ReceiveMessage", serializeMessages);
                _responseModel.isSuccess = true;
                return _responseModel;
            }
            _responseModel.isSuccess = false;
            return _responseModel;
        }
    }
}
