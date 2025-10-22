using System;

namespace ThermostatEventsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Implementation for ThermostatEventsApp goes here
        }
    }

    public class HeatSensor : IHeatSensor
    {
        double _warningLevel;
        double _emergencyLevel;
        bool _hasCrossedWarningLevel = false;

        public HeatSensor(double warningLevel, double emergencyLevel)
        {
            _warningLevel = warningLevel;
            _emergencyLevel = emergencyLevel;
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachEmergencyLevel
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureAboveWarningLevel
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureBelowWarningLevel
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        public void RunHeatSensor()
        {
            throw new NotImplementedException();
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