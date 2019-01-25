using EventPlanning.BLL.Interfaces;
using EventPlanning.DAL.Repositories;

namespace EventPlanning.BLL.Services
{
    public class ServiceCreator: IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
    }
}
