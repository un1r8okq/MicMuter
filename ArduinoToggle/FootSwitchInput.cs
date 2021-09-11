using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArduinoToggle
{
    class FootSwitchInput : BooleanInput
    {
        private readonly CancellationTokenSource _cancelTokenSource;
        private readonly SerialPort _serialPort;
        private Action<bool> _valueChanged = (value) => { };

        public FootSwitchInput()
        {
            _cancelTokenSource = new CancellationTokenSource();
            _serialPort = GetSerialPort();
            _serialPort.Open();
            Task.Run(() => ListenToSwitch(), _cancelTokenSource.Token);
        }

        public void Dispose()
        {
            Console.WriteLine($"Cancelling serial polling");
            _cancelTokenSource.Cancel();
            Console.WriteLine($"Closing serial port {_serialPort.PortName}");
            _serialPort.Close();
        }

        public void OnValueChanged(Action<bool> valueChangedAction)
        {
            _valueChanged = valueChangedAction;
        }

        private SerialPort GetSerialPort()
        {
            var firstSerialPort = SerialPort.GetPortNames().FirstOrDefault();

            if (firstSerialPort is null)
            {
                throw new MissingSerialPortException();
            }

            Console.WriteLine($"Connecting to serial port {firstSerialPort}");

            return new SerialPort(firstSerialPort, 9600);
        }

        private void ListenToSwitch()
        {
            bool? prevState = null;

            while (!_cancelTokenSource.IsCancellationRequested)
            {
                var line = _serialPort.ReadLine();
                var currentState = line[0] == '1';

                if (currentState != prevState)
                {
                    _valueChanged(currentState);
                }

                prevState = currentState;
            }
        }
    }
}
