using EventPlanning.DAL.Entities;
using System.Collections.Generic;

namespace EventPlanning.DAL.Interfaces
{
    public interface IParticipantRepository : IRepository<Participant>
    {
        IEnumerable<Participant> GetByEventId(int id);
    }
}
