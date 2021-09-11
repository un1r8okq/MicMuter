using System;

namespace ArduinoToggle
{
    interface IBooleanInput: IDisposable
    {
        public void OnValueChanged(Action<bool> valueChangedAction);
    }
}
