using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using Windows.Media.Capture;

namespace ArduinoToggle
{
    class Program
    {
        // TODO: Log time alongside error messages
        // TODO: Improve resiliance when serial connection lost
        // TODO: Improve latency
        // TODO: Mutes all microphones, not just the current one.
        // TODO: Collect stats on use? Time spent muted/unmuted, total mute/unmutes? Graph over time?
        static async Task Main()
        {
            var unmutedSound = new SoundPlayer(Path.Combine("audio", "unmuted.wav"));
            var mutedSound = new SoundPlayer(Path.Combine("audio", "muted.wav"));

            var mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();

            new SwitchListener()
                .OnSwitchChanged((switchIsClosed) =>
                    {
                        if (switchIsClosed)
                        {
                            Console.WriteLine("Muted");
                            mediaCapture.AudioDeviceController.Muted = true;
                            mutedSound.Play();
                        }
                        else
                        {
                            Console.WriteLine("Unmuted");
                            mediaCapture.AudioDeviceController.Muted = false;
                            unmutedSound.Play();
                        }
                    })
                .Listen();
        }
    }
}
