using System;
using System.Collections.Generic;

namespace MicMuter.Events
{
    class EventCollector
    {
        private readonly List<Event> _events = new();

        public void Send(Event newEvent)
        {
            _events.Add(newEvent);
        }

        public void ListEvents()
        {
            foreach (var e in _events)
            {
                Console.WriteLine(e);
            }
        }
    }
}
