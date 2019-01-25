using EventPlanning.BLL.DTO;
using System.Collections.Generic;

namespace EventPlanning.BLL.Interfaces
{
    public interface IParticipantService
    {
        IEnumerable<ParticipantDTO> FindByEventId(int? id);
        void AddParticapant(ParticipantDTO item); 
    }
}