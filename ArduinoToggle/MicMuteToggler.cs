using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using Windows.Media.Capture;

namespace ArduinoToggle
{
    class MicMuteToggler: IDisposable
    {
        private readonly SoundPlayer _mutedSound;
        private readonly SoundPlayer _unmutedSound;
        private MediaCapture _mediaCapture;

        public MicMuteToggler(IBooleanInput booleanInput)
        {
            _unmutedSound = new SoundPlayer(Path.Combine("audio", "unmuted.wav"));
            _mutedSound = new SoundPlayer(Path.Combine("audio", "muted.wav"));

            booleanInput.OnValueChanged(async (newValue) =>
            {
                if (newValue)
                {
                    await SetMuted(true);
                    _mutedSound.Play();
                }
                else
                {
                    await SetMuted(false);
                    _unmutedSound.Play();
                }
            });
        }

        public void Dispose()
        {
            _mediaCapture.Dispose();
            _mutedSound.Dispose();
            _unmutedSound.Dispose();
        }

        private async Task SetMuted(bool isMuted)
        {
            if (_mediaCapture is null)
            {
                _mediaCapture = new MediaCapture();
                await _mediaCapture.InitializeAsync();
            }
            _mediaCapture.AudioDeviceController.Muted = isMuted;
        }
    }
}
