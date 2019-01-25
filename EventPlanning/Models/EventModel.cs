using System;
using System.Collections.Generic;

namespace EventPlanning.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<FieldModel> Fields { get; set; }

        public virtual ICollection<ParticipantModel> Participants { get; set; }


        public EventModel()
        {
            Fields = new List<FieldModel>();
        }
    }
}