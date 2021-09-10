using System;
using System.IO.Ports;
using System.Linq;

namespace ArduinoToggle
{
    class SwitchListener
    {
        private Action<bool> _switchChanged = (value) => { };

        public SwitchListener OnSwitchChanged(Action<bool> switchChangedAction)
        {
            _switchChanged = switchChangedAction;
            return this;
        }

        public void Listen()
        {
            var serialPort = GetSerialPort();
            serialPort.Open();

            bool? prevState = null;

            while (true)
            {
                var line = serialPort.ReadLine();
                var currentState = line[0] == '1';

                if (currentState != prevState)
                {
                    _switchChanged(currentState);
                }

                prevState = currentState;
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
