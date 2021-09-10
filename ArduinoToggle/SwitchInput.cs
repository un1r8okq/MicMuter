using System;
using System.IO.Ports;
using System.Linq;

namespace ArduinoToggle
{
    class SwitchInput
    {
        private Action _switchClosed = () => { };
        private Action _switchOpened = () => { };

        public void OnSwitchClosed(Action action) => _switchClosed = action;

        public void OnSwitchOpened(Action action) => _switchOpened = action;

        public void Run()
        {
            var serialPort = GetSerialPort();
            serialPort.Open();

            bool? switchWasClosed = null;

            while (true)
            {
                var line = serialPort.ReadLine();
                var switchIsClosed = line[0] == '1';

                if (switchIsClosed != switchWasClosed)
                {
                    if (switchIsClosed)
                    {
                        _switchClosed();
                    }
                    else
                    {
                        _switchOpened();
                    }
                }

                switchWasClosed = switchIsClosed;
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
