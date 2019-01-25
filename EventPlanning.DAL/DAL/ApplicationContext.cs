using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using EventPlanning.DAL.Entities;

namespace EventPlanning.DAL.DAL
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
