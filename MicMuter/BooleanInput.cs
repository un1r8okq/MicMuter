using System;

namespace MicMuter
{
    interface IBooleanInput: IDisposable
    {
        public void OnValueChanged(Action<bool> valueChangedAction);
    }
}
