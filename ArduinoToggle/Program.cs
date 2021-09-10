using System;
using System.IO.Ports;
using System.Media;
using System.Threading.Tasks;
using Windows.Media.Capture;

namespace ArduinoToggle
{
    class Program
    {
        // TODO: Scan ports for the right serial device
        // TODO: Mutes all microphones, not just the current one.
        // TODO: Improve latency
        // TODO: Collect stats on use? Time spent muted/unmuted, total mute/unmutes? Graph over time?
        static async Task Main()
        {
            var mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();
            var unmutedSound = new SoundPlayer("audio/unmuted.wav");
            var mutedSound = new SoundPlayer("audio/muted.wav"); 
            var port = new SerialPort("COM4")
            {
                BaudRate = 9600
            };
            port.Open();

            bool? switchWasClosed = null;

            while (true)
            {
                var switchIsClosed = port.ReadLine()[0] == '1';

                if (switchIsClosed != switchWasClosed)
                {
                    if (switchIsClosed)
                    {
                        Console.WriteLine($"Muted");
                        mediaCapture.AudioDeviceController.Muted = true;
                        mutedSound.Play();
                    }
                    else
                    {
                        Console.WriteLine($"Unmuted");
                        mediaCapture.AudioDeviceController.Muted = false;
                        unmutedSound.Play();
                    }
                }

                switchWasClosed = switchIsClosed;
            }
        }
    }
}
