using EventPlanning.DAL.Annotations;
using EventPlanning.DAL.Entities;
using System.Data.Entity;

namespace EventPlanning.DAL.DAL
{
    public class EventContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Participant> Participants { get; set; }

     
        public EventContext(string connectionString)
            : base(connectionString)
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new DataTypePropertyAttributeConvention());
            modelBuilder.Entity<Field>().HasOptional(i => i.Event).WithMany(i => i.Fields).WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}