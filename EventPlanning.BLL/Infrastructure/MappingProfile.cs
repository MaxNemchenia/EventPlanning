using AutoMapper;
using EventPlanning.BLL.DTO;
using EventPlanning.DAL.Entities;

namespace EventPlanning.BLL.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<EventDTO, Event>()
                .ForMember(dest => dest.Participants, opt => opt.MapFrom(source => source.Participants))
                .ReverseMap();
            this.CreateMap<FieldDTO, Field>().ReverseMap();
            this.CreateMap<ParticipantDTO, Participant>().ReverseMap();

            this.CreateMap<UserDTO, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(source => source.Email))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(source => source.ConfirmedEmail))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(source => source.Password))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(source => source.UserName))
                .ReverseMap();
        }
    }
}
