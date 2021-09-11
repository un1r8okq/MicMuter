using System;

namespace ArduinoToggle
{
    interface BooleanInput: IDisposable
    {
        public void OnValueChanged(Action<bool> valueChangedAction);
    }
}
