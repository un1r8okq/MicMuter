namespace ArduinoToggle.Events
{
    class SerialPortOpenedEvent : Event
    {
        public SerialPortOpenedEvent(string portName): base($"Serial port {portName} opened") {}
    }
}
