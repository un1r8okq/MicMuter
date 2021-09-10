using System;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using Windows.Media.Capture;

namespace ArduinoToggle
{
    class Program
    {
        // TODO: Mutes all microphones, not just the current one.
        // TODO: Improve latency
        // TODO: Collect stats on use? Time spent muted/unmuted, total mute/unmutes? Graph over time?
        static async Task Main()
        {
            var mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();
            var unmutedSound = new SoundPlayer("audio/unmuted.wav");
            var mutedSound = new SoundPlayer("audio/muted.wav");
            var port = GetSerialPort(); 
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

        private static SerialPort GetSerialPort()
        {
            var firstSerialPort = SerialPort.GetPortNames().FirstOrDefault();

            if (firstSerialPort is null)
            {
                throw new MissingSerialPortException();
            }

            Console.WriteLine($"Connecting to serial port {firstSerialPort}");

            return new SerialPort(firstSerialPort, 9600);
        }
    }
}
