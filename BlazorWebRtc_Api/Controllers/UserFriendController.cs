﻿using BlazorWebRtc_Application.Features.Commands.UserFriendFeature;
using BlazorWebRtc_Application.Features.Queries.UserFriend;
using BlazorWebRtc_Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFriendController : ControllerBase
    {
        private readonly IUserFriendService _userFriendService;
        public UserFriendController(IUserFriendService userFriendService)
        {
            _userFriendService = userFriendService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserFriend(UserFriendCommand command)
        {
            return Ok(await _userFriendService.AddFriendship(command)); 
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserFriend(DeleteFriendshipCommand command)
        {
            return Ok(await _userFriendService.DeleteFriendship(command));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFriendship()
        {
            UserFriendListCommand query = new UserFriendListCommand();
            return Ok(await _userFriendService.GetFriendshipList(query));
        }
    }
}
