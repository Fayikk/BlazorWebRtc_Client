using BlazorWebRtc_Application.DTO.Request;
using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Application.Features.Queries.RequestFeature
{
    public class RequestsHandler : IRequestHandler<RequestsQuery, List<GetRequestDTO>>
    {
        private readonly AppDbContext _context;
        public RequestsHandler(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<List<GetRequestDTO>> Handle(RequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _context.Requests.Include(x=>x.SenderUser).Where(x => x.ReceiverUserId == request.UserId && x.Status == Status.pending).ToListAsync();
            List<GetRequestDTO> requestList = new();
            foreach (var item in requests)
            {
                GetRequestDTO requestDTO = new();
                requestDTO.Id = item.Id;
                requestDTO.ProfilePicture = item.SenderUser.ProfilePicture;
                requestDTO.UserName = item.SenderUser.UserName; 
                requestDTO.Email = item.SenderUser.Email;
                requestDTO.UserId = item.SenderUserId;
                requestList.Add(requestDTO);    
            }
            if (requestList.Any())
            {
                return requestList;
            }
            return null;
        }
    }
}
