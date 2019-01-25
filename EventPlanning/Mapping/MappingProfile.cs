using AutoMapper;
using EventPlanning.BLL.DTO;
using EventPlanning.Models;

namespace EventPlanning.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            this.CreateMap<EventDTO, EventModel>()
                .ForMember(dest => dest.Participants, opt => opt.MapFrom(source => source.Participants))
                .ReverseMap();

            this.CreateMap<FieldDTO, FieldModel>().ReverseMap();
        }
    }
}
