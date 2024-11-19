using BlazorWebRtc_Application.Features.Commands.UserFriendFeature;
using BlazorWebRtc_Application.Features.Queries.UserFriend;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Services
{
    public class UserFriendService : IUserFriendService
    {
        private readonly IMediator mediator;
        private readonly BaseResponseModel responseModel;
        public UserFriendService(IMediator mediator, BaseResponseModel responseModel)
        {
            this.mediator = mediator;
            this.responseModel = responseModel;
        }

        public async Task<BaseResponseModel> AddFriendship(UserFriendCommand command)
        {
            var result = await mediator.Send(command);
            if (result)
            {
                responseModel.isSuccess = true;
                return responseModel;
            }
            responseModel.isSuccess = false;
            return responseModel;
        }

        public async Task<BaseResponseModel> DeleteFriendship(DeleteFriendshipCommand command)
        {
            var result = await mediator.Send(command);
            if (result)
            {
                responseModel.isSuccess = true;
                return responseModel;
            }
            responseModel.isSuccess = false;
            return responseModel;
        }

        public async Task<BaseResponseModel> GetFriendshipList(UserFriendListCommand query)
        {


            var result = await mediator.Send(query);
            responseModel.isSuccess = true;
            responseModel.Data = result;
            return responseModel;
        }
    }
}
