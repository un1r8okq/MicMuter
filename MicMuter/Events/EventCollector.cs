using System;
using System.Collections.Generic;
using System.IO;

namespace MicMuter.Events
{
    class EventCollector
    {
        private readonly List<Event> _events = new();

        public void Send(Event newEvent)
        {
            _events.Add(newEvent);
        }

        public void PrintEventsToConsole()
        {
            foreach (var e in _events)
            {
                Console.WriteLine($"{e.DateTimeUtc:yyyy-MM-ddTHH:mm:ss:fffZ} {e.EventType}");
            }
        }

        public void PrintEventsToCsv()
        {
            var directoryPath = Path.Combine("C:", "temp", "MicMuter");
            var filePath = Path.Combine(directoryPath, $"{DateTime.UtcNow:yyyy-MM-ddTHH-mm-ss}.csv");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using var fileStream = File.CreateText(filePath);
            fileStream.WriteLine("dateTime,eventType");

            foreach (var e in _events)
            {
                fileStream.WriteLine($"{e.DateTimeUtc:yyyy-MM-ddTHH:mm:ss:fff},{e.EventType}");
            }

            fileStream.Close();

            Console.WriteLine($"Events written to {filePath}");
        }
    }
}
