using System;

namespace MicMuter.Events
{
    abstract class Event
    {
        public DateTime DateTimeUtc { get; set; }
        public string EventType { get; set; }

        public Event(string eventType)
        {
            DateTimeUtc = DateTime.UtcNow;
            EventType = eventType;
        }
    }
}
