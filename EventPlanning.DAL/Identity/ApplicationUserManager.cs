using Microsoft.AspNet.Identity;
using EventPlanning.DAL.Entities;

namespace EventPlanning.DAL.Identity
{
    public class ApplicationUserManager:UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
               : base(store)
        {
        }
    }
}
