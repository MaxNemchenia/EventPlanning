namespace EventPlanning.Models
{
    public class ParticipantModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public int? EventId { get; set; }
    }
}