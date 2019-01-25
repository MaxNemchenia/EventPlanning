using System;
using System.Collections.Generic;
namespace EventPlanning.BLL.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<FieldDTO> Fields { get; set; }

        public virtual ICollection<ParticipantDTO> Participants { get; set; }
    }
}
