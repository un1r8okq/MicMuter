using MicMuter.Events;
using System;

namespace MicMuter
{
    class Program
    {
        // TODO: Improve resiliance when serial connection lost
        // TODO: Improve latency
        // TODO: Mutes all microphones, not just the current one.
        static void Main()
        {
            var eventCollector = new EventCollector();
            using var input = new FootSwitchInput(eventCollector);
            using var output = new MicMuteToggler(input);

            while (true)
            {
                Console.WriteLine("Press q to quit, e to list events, or s to save events as CSV");
                var keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.E)
                {
                    eventCollector.PrintEventsToConsole();
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.S)
                {
                    eventCollector.PrintEventsToCsv();
                }
                Console.WriteLine();
            }
        }
    }
}
