using EventPlanning.DAL.DAL;
using EventPlanning.DAL.Entities;
using EventPlanning.DAL.Interfaces;
using EventPlanning.DAL.Repositories;
using Ninject.Modules;

namespace EventPlanning.BLL.Infrastructure
{
    public class ServiceModule:NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IEventRepository>().To<EventRepository>().WithConstructorArgument(connectionString);
            Bind<IParticipantRepository>().To<ParticipantRepository>().WithConstructorArgument(connectionString);
        }
    }
}
