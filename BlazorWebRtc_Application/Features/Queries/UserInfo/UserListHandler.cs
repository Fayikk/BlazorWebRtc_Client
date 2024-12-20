﻿using BlazorWebRtc_Application.DTO;
using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebRtc_Application.Features.Queries.UserInfo
{
    public class UserListHandler : IRequestHandler<UserListQuery, List<UserDTO>>
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private string? userId;
        public UserListHandler(AppDbContext context,IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<List<UserDTO>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            List<User> users = await _context.Users.ToListAsync(cancellationToken);
            userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (users != null) { 
                List<UserDTO> userDTOs = new List<UserDTO>();
                foreach (var item in users)
                {

                    if (userId is not null && userId != item.Id.ToString())
                    {
                        UserDTO userDTO = new UserDTO();
                        userDTO.UserName = item.UserName;
                        userDTO.Email = item.Email;
                        userDTO.ProfilePicture = item.ProfilePicture;
                        userDTO.UserId = item.Id;
                        userDTOs.Add(userDTO);
                    }

                 
                }

                return userDTOs;
            }
            return null;

        }
    }
}
