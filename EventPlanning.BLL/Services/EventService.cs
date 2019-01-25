using EventPlanning.BLL.DTO;
using EventPlanning.BLL.Interfaces;
using EventPlanning.DAL.Interfaces;
using System.Collections.Generic;
using EventPlanning.DAL.Entities;
using AutoMapper;

namespace EventPlanning.BLL.Services
{
    public class EventService:IEventService
    {
        IEventRepository repository;

        public EventService(IEventRepository repository)
        {
            this.repository = repository;
        }



        public IEnumerable<EventDTO> GetEvents()
        {
            IEnumerable<Event> events = repository.GetAll();
            var eventDTOs = Mapper.Map<IEnumerable<Event>, List<EventDTO>>(events);
            return eventDTOs;
        }


        public EventDTO GetById(int? id)
        {
            if (id != null && repository.GetById((int)id) != null)
            {
                Event @event = repository.GetById((int)id);
                var eventDTO = Mapper.Map<Event, EventDTO>(@event);
                return eventDTO;
            }

            return null;
        }
      

        public void AddEvent(EventDTO eventDTO)
        {
            var @event = Mapper.Map<EventDTO, Event>(eventDTO);
            repository.Create(@event);
        }


        public void EditEvent(EventDTO eventDTO)
        {
            var @event = Mapper.Map<EventDTO, Event>(eventDTO);
            repository.Update(@event);
        }


        public bool DeleteEvent(int? id)
        {
            if (id != null && repository.Delete((int)id))
                return true;

            return false;
        }
    }
}
