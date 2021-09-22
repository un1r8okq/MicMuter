using MicMuter.Events;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace MicMuter
{
    class FootSwitchInput : IBooleanInput
    {
        private readonly CancellationTokenSource _cancelTokenSource;
        private readonly SerialPort _serialPort;
        private readonly EventCollector _eventCollector;
        private Action<bool> _valueChanged = (value) => { };

        public FootSwitchInput(EventCollector eventCollector)
        {
            _eventCollector = eventCollector;
            _cancelTokenSource = new CancellationTokenSource();

            _serialPort = GetSerialPort();
            _serialPort.Open();
            Task.Run(() => ListenToSwitch(), CancellationToken.None);
        }

        public void Dispose()
        {
            _cancelTokenSource.Cancel();
            _eventCollector.Send(new SerialPortClosedEvent(_serialPort.PortName));
            _serialPort.Close();
        }

        public void OnValueChanged(Action<bool> valueChangedAction)
        {
            _valueChanged = valueChangedAction;
        }

        private SerialPort GetSerialPort()
        {
            var firstSerialPort = SerialPort.GetPortNames()[0];

            if (firstSerialPort is null)
            {
                throw new MissingSerialPortException();
            }

            var serialport = new SerialPort(firstSerialPort, 9600);
            _eventCollector.Send(new SerialPortOpenedEvent(serialport.PortName));
            return serialport;
        }

        private void ListenToSwitch()
        {
            bool? prevState = null;

            while (!_cancelTokenSource.IsCancellationRequested)
            {
                var line = _serialPort.ReadLine();
                if (line.Length == 0)
                {
                    continue;
                }
                var currentState = line[0] == '1';

                if (currentState != prevState)
                {
                    _eventCollector.Send(currentState ? new SwitchClosedEvent() : new SwitchOpenedEvent());
                    _valueChanged(currentState);
                }

                prevState = currentState;
            }
        }
    }
}
