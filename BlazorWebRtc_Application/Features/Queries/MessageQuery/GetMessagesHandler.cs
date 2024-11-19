using AutoMapper;
using BlazorWebRtc_Application.DTO.Message;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Queries.MessageQuery
{
    public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, List<MessageDTO>>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        public GetMessagesHandler(IMapper mapper,IHttpContextAccessor contextAccessor,AppDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext; 
            this.contextAccessor = contextAccessor; 
        }

        public async Task<List<MessageDTO>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var userId =  contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var messages = await dbContext.MessagesRooms
                    .Where(x=>(x.SenderUserId == Guid.Parse(userId) || x.ReceiverUserId == Guid.Parse(userId)) 
                        && (x.ReceiverUserId == Guid.Parse(request.MessageUserId) || x.SenderUserId == Guid.Parse(request.MessageUserId))).OrderBy(x=>x.CreateDate).ToListAsync();

            if (messages.Any()) {

                var result = mapper.Map<List<MessageDTO>>(messages);

                return result;
            
            }
            return new List<MessageDTO>();

        }
    }
}
