using EventPlanning.Models;
namespace EventPlanning
{
    public class EventParser
    {
        public static EventModel Parse(ViewEventModel viewEventModel)
        {
            EventModel @event = new EventModel();
            @event.Id = viewEventModel.Id;
            @event.Name = viewEventModel.Name;
            @event.Date = viewEventModel.Date;
            if (viewEventModel.Names != null)
            {
                for (int i = 0; i < viewEventModel.Names.Length; i++)
                {
                    @event.Fields.Add(new FieldModel() { EventId = @event.Id, Name = viewEventModel.Names[i], Value = viewEventModel.Values[i] });
                }
            }

            return @event;
        }
    }
}