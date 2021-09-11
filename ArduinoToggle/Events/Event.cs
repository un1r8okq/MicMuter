using System;

namespace ArduinoToggle.Events
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

        public override string ToString()
        {
            return $"{DateTimeUtc:yyyy-MM-ddTHH:mm:ss:FFFZ} {EventType}";
        }
    }
}
