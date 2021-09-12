namespace MicMuter.Events
{
    class SerialPortClosedEvent : Event
    {
        public SerialPortClosedEvent(string portName): base($"Serial port {portName} closed") {}
    }
}
