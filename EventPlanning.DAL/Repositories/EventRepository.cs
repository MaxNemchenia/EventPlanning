using EventPlanning.DAL.DAL;
using EventPlanning.DAL.Entities;
using EventPlanning.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;



namespace EventPlanning.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private EventContext db;


        public EventRepository(string connectionString)
        {
            this.db = new EventContext(connectionString);
        }


        public void Create(Event @event)
        {
            db.Events.Add(@event);
            db.SaveChanges();
        }

        public bool Delete(int id)
        {
            Event @event = db.Events.Find(id);
            if (@event != null)
            {
                db.Events.Remove(@event);
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public Event GetById(int id)
        {
            return db.Events.Find(id);
        }

        public IEnumerable<Event> GetAll()
        {
            return db.Events.Include("Fields").ToList();
        }

        public void Update(Event @event)
        {
            Event oldEvent = db.Events.Find(@event.Id);
            oldEvent.Name = @event.Name;
            var fields = db.Fields.Where(i => i.EventId == @event.Id).ToList();
            db.Fields.RemoveRange(fields);
            db.Fields.AddRange(@event.Fields);
            db.Entry(oldEvent).CurrentValues.SetValues(@event);
            db.SaveChanges();
        }
    }
}
