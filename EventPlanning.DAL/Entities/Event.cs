using System;
using System.Collections.Generic;

namespace EventPlanning.DAL.Entities
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Field> Fields { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }


        public Event()
        {
            Fields = new List<Field>();
        }
    }
}
