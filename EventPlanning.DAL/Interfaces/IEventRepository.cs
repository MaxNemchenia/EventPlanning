using EventPlanning.DAL.Entities;
using System.Collections.Generic;

namespace EventPlanning.DAL.Interfaces
{
    public interface IEventRepository : IRepository<Event> 
    {
        IEnumerable<Event> GetAll();
        Event GetById(int id);
        void Update(Event item);
        bool Delete(int id);
    }
}
