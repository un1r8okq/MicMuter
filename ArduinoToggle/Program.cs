using System;
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
            var unmutedSound = new SoundPlayer("audio/unmuted.wav");
            var mutedSound = new SoundPlayer("audio/muted.wav");

            var mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();

            var switchInput = new SwitchInput();
            switchInput.OnSwitchClosed(() =>
            {
                Console.WriteLine($"Muted");
                mediaCapture.AudioDeviceController.Muted = true;
                mutedSound.Play();
            });
            switchInput.OnSwitchOpened(() =>
            {
                Console.WriteLine($"Unmuted");
                mediaCapture.AudioDeviceController.Muted = false;
                unmutedSound.Play();
            });
            switchInput.Run();
        }
    }
}
