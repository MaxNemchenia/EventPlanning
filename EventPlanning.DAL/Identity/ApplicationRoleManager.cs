using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EventPlanning.DAL.Entities;

namespace EventPlanning.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
                    : base(store)
        { }
    }
}
