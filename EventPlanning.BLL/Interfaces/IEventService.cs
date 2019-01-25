using EventPlanning.BLL.DTO;
using System.Collections.Generic;

namespace EventPlanning.BLL.Interfaces
{
    public interface IEventService
    {
        IEnumerable<EventDTO> GetEvents();
        EventDTO GetById(int? id);
        void AddEvent(EventDTO eventDTO);
        void EditEvent(EventDTO eventDTO);
        bool DeleteEvent(int? id);
    }
}
