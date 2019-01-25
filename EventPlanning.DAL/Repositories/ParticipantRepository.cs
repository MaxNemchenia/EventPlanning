using EventPlanning.DAL.DAL;
using EventPlanning.DAL.Entities;
using EventPlanning.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EventPlanning.DAL.Repositories
{
    public class ParticipantRepository:IParticipantRepository
    {
        private EventContext db;


        public ParticipantRepository(string connectionString)
        {
            this.db = new EventContext(connectionString);
        }


        public void Create(Participant item)
        {
            db.Participants.Add(item);
            db.SaveChanges();
        }

        public IEnumerable<Participant> GetByEventId(int id)
        {
            var participants = db.Participants.Where(p => p.EventId == id).ToList();
            return participants;
        }
    }
}
