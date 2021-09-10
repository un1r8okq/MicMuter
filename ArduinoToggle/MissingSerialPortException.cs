using System;

namespace ArduinoToggle
{
    [Serializable]
    internal class MissingSerialPortException : Exception
    {
        public MissingSerialPortException() :
            base("Unable to find a serial port to communicate with the Ardunio")
        {}
    }
}
