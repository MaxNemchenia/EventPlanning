namespace EventPlanning.DAL.Entities
{
    public class Field
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int? EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
