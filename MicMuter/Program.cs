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
                Console.WriteLine("Press q to quit or e to list events");
                var keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.E)
                {
                    eventCollector.ListEvents();
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    break;
                }
                Console.WriteLine();
            }
        }
    }
}
