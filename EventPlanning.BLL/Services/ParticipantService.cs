using AutoMapper;
using EventPlanning.BLL.DTO;
using EventPlanning.BLL.Interfaces;
using EventPlanning.DAL.Entities;
using EventPlanning.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.BLL.Services
{
    public class ParticipantService:IParticipantService
    {
        IParticipantRepository repository;

        public ParticipantService(IParticipantRepository repository)
        {
            this.repository = repository;
        }



        public void AddParticapant(ParticipantDTO participantDTO)
        {
            var participant = Mapper.Map<ParticipantDTO, Participant>(participantDTO);
            repository.Create(participant);
        }


        public IEnumerable<ParticipantDTO> FindByEventId(int? id)
        {
            if (id != null)
            {
                var participants = Mapper.Map<IEnumerable<Participant>, IEnumerable<ParticipantDTO>>(repository.GetByEventId((int)id));
                return participants;
            }

            return null;
        }
    }
}
