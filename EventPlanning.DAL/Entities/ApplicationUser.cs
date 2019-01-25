using Microsoft.AspNet.Identity.EntityFramework;

namespace EventPlanning.DAL.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
        public string Password { get; set; }
    }
}
