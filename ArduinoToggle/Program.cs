using System;

namespace ArduinoToggle
{
    class Program
    {
        // TODO: Log time alongside error messages
        // TODO: Improve resiliance when serial connection lost
        // TODO: Improve latency
        // TODO: Mutes all microphones, not just the current one.
        // TODO: Collect stats on use? Time spent muted/unmuted, total mute/unmutes? Graph over time?
        static void Main()
        {
            using var input = new FootSwitchInput();
            using var output = new MicMuteToggler(input);

            Console.ReadLine();
        }
    }
}
