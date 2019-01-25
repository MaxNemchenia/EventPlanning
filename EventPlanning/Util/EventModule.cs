using EventPlanning.BLL.Interfaces;
using EventPlanning.BLL.Services;
using Ninject.Modules;

namespace EventPlanning.Util
{
    public class EventModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IEventService>().To<EventService>();
            Bind<IParticipantService>().To<ParticipantService>();
        }
    }
}