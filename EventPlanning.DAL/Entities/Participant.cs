namespace EventPlanning.DAL.Entities
{
    public class Participant
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public virtual Event Event { get; set; }
        public int? EventId { get; set; }
    }
}
