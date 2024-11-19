using BlazorWebRtc_Application.DTO.UserFriend;
using MediatR;

namespace BlazorWebRtc_Application.Features.Queries.UserFriend
{
    public class UserFriendListCommand : IRequest<List<UserFriendDTO>>
    {
    }
}
