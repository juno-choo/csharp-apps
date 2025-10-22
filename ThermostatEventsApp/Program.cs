using System;
using System.ComponentModel;

namespace ThermostatEventsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Implementation for ThermostatEventsApp goes here
            Console.WriteLine("Press any key to start the device...");
            Console.ReadKey();
            Console.WriteLine();

            IDevice device = new Device();

            device.RunDevice();
            Console.WriteLine("Device operation completed. Press any key to exit.");
            Console.ReadKey();
        }
    }

    public class Device : IDevice
    {
        const double WARNING_TEMPERATURE_LEVEL = 27;
        const double EMERGENCY_TEMPERATURE_LEVEL = 75;

        public double WarningTemperatureLevel => WARNING_TEMPERATURE_LEVEL;
        public double EmergencyTemperatureLevel => EMERGENCY_TEMPERATURE_LEVEL;

        public void RunDevice()
        {
            Console.WriteLine("Device is running...");
            Console.WriteLine();
            ICoolingMechanism coolingMechanism = new CoolingMechanism();
            IHeatSensor heatSensor = new HeatSensor(WARNING_TEMPERATURE_LEVEL, EMERGENCY_TEMPERATURE_LEVEL);
            IThermostat thermostat = new Thermostat(this, heatSensor, coolingMechanism);
            thermostat.RunThermostat();
        }

        public void HandleEmergency()
        {
            Console.WriteLine("Handling emergency! Shutting down device...");
            ShutDownDevice();
            Console.WriteLine();
        }

        private void ShutDownDevice()
        {
            Console.WriteLine("Device shut down.");
        }
    }

    public class Thermostat : IThermostat
    {
        private ICoolingMechanism _coolingMechanism = null;
        private IHeatSensor _heatSensor = null;
        private IDevice _device = null;

        public Thermostat(IDevice device, IHeatSensor heatSensor, ICoolingMechanism coolingMechanism)
        {
            _device = device;
            _heatSensor = heatSensor;
            _coolingMechanism = coolingMechanism;

        }

        private void WireUpEventHandlers()
        {
            _heatSensor.TemperatureAboveWarningLevel += HeatSensor_TemperatureAboveWarningLevel;
            _heatSensor.TemperatureBelowWarningLevel += HeatSensor_TemperatureBelowWarningLevel;
            _heatSensor.TemperatureReachEmergencyLevel += HeatSensor_TemperatureReachEmergencyLevel;
        }

        private void HeatSensor_TemperatureReachEmergencyLevel(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"EMERGENCY! Temperature reached {_device.EmergencyTemperatureLevel}°C at {e.CurrentDateTime}");
            _device.HandleEmergency();
            Console.ResetColor();
        }

        private void HeatSensor_TemperatureBelowWarningLevel(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Temperature back to normal: {_device.WarningTemperatureLevel}°C at {e.CurrentDateTime}");
            _coolingMechanism.Off();
            Console.ResetColor();
        }

        private void HeatSensor_TemperatureAboveWarningLevel(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Warning! Temperature above normal: {_device.WarningTemperatureLevel}°C at {e.CurrentDateTime}");
            _coolingMechanism.On();
            Console.ResetColor();
        }

        public void RunThermostat()
        {
            Console.WriteLine("Starting thermostat...");
            WireUpEventHandlers();
            _heatSensor.RunHeatSensor();
        }
    }

    public interface IThermostat
    {
        void RunThermostat();
    }

    public interface IDevice
    {
        double WarningTemperatureLevel { get; }
        double EmergencyTemperatureLevel { get; }   
        void RunDevice();
        void HandleEmergency();
    }

    public class CoolingMechanism : ICoolingMechanism
    {
        public void On()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Cooling mechanism ON");
            Console.WriteLine();
        }

        public void Off()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cooling mechanism OFF");
            Console.WriteLine();
        }
    }

    public interface ICoolingMechanism
    {
        void On();
        void Off();
    }

    public class HeatSensor : IHeatSensor
    {
        double _warningLevel;
        double _emergencyLevel;
        bool _hasCrossedWarningLevel = false;
        protected EventHandlerList _listEventDelegates = new EventHandlerList();

        static readonly object _temperatureReachEmergencyLevelEventKey = new object();
        static readonly object _temperatureAboveWarningLevelEventKey = new object();
        static readonly object _temperatureBelowWarningLevelEventKey = new object();

        private double[] _temperatureData = null;
        public HeatSensor(double warningLevel, double emergencyLevel)
        {
            _warningLevel = warningLevel;
            _emergencyLevel = emergencyLevel;

            SeedData();
        }

        private void MonitorTemperature()
        {
            foreach (var temp in _temperatureData)
            {
                Console.ResetColor();
                Console.WriteLine($"Current Temperature: {temp}°C");
                if (temp >= _emergencyLevel)
                {
                    TemperatureEventArgs e = new TemperatureEventArgs();
                    e.Temperature = temp;
                    e.CurrentDateTime = DateTime.Now;
                    OnTemperatureReachEmergencyLevel(e);
                }
                else if (temp >= _warningLevel)
                {
                    if (!_hasCrossedWarningLevel)
                    {
                        TemperatureEventArgs e = new TemperatureEventArgs();
                        e.Temperature = temp;
                        e.CurrentDateTime = DateTime.Now;
                        OnTemperatureAboveWarningLevel(e);
                        _hasCrossedWarningLevel = true;
                    }
                }
                else
                {
                    if (_hasCrossedWarningLevel)
                    {
                        TemperatureEventArgs e = new TemperatureEventArgs();
                        e.Temperature = temp;
                        e.CurrentDateTime = DateTime.Now;
                        OnTemperatureBelowWarningLevel(e);
                        _hasCrossedWarningLevel = false;
                    }
                }

                System.Threading.Thread.Sleep(1000);
            }
        }

        private void SeedData()
        {
            _temperatureData = new double[] { 16.0, 18.5, 24.0, 26, 27, 28.8, 26, 22, 48, 75, 81, 68, 45 };
        }

        protected void OnTemperatureReachEmergencyLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureReachEmergencyLevelEventKey];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnTemperatureAboveWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureAboveWarningLevelEventKey];
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected void OnTemperatureBelowWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureBelowWarningLevelEventKey];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachEmergencyLevel
        {
            add { _listEventDelegates.AddHandler(_temperatureReachEmergencyLevelEventKey, value); }
            remove { _listEventDelegates.RemoveHandler(_temperatureReachEmergencyLevelEventKey, value); }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureAboveWarningLevel
        {
            add { _listEventDelegates.AddHandler(_temperatureAboveWarningLevelEventKey, value); }
            remove { _listEventDelegates.RemoveHandler(_temperatureAboveWarningLevelEventKey, value); }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureBelowWarningLevel
        {
            add { _listEventDelegates.AddHandler(_temperatureBelowWarningLevelEventKey, value); }
            remove { _listEventDelegates.RemoveHandler(_temperatureBelowWarningLevelEventKey, value); }
        }

        public void RunHeatSensor()
        {
            System.Console.WriteLine("Running heat sensor...");
            MonitorTemperature();
        }
    }
    public interface IHeatSensor
    {
        event EventHandler<TemperatureEventArgs> TemperatureReachEmergencyLevel;
        event EventHandler<TemperatureEventArgs> TemperatureAboveWarningLevel;
        event EventHandler<TemperatureEventArgs> TemperatureBelowWarningLevel;

        void RunHeatSensor();
    }

    public class TemperatureEventArgs : EventArgs
    {
        public double Temperature { get; set; }
        public DateTime CurrentDateTime { get; set; }
    }
}