using System.Collections.Generic;

namespace Heatmap.Readers
{
    using Events;
    internal interface IEventReader
    {
        List<EventsContainer> ReadEvents();
    }
}