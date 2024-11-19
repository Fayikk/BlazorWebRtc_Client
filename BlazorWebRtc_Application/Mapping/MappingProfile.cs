using AutoMapper;
using BlazorWebRtc_Application.DTO.Message;
using BlazorWebRtc_Domain;

namespace BlazorWebRtc_Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MessageRoom, MessageDTO>().ReverseMap();
        }
    }
}
