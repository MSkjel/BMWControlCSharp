using BMWControl.CanEvents;
using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.Handlers.CarHandlers
{
    public class ClimateHandler : ICanEvent
    {
        public struct TemperatureStruct
        {
            public int DriverTemperatureSetpoint;
            public int PassengerTemperatureSetpoint;

            public int InteriorTemperatureClimate;
            public int InteriorTemperatureDriverActual;
            public int InteriorTemperaturePassengerActual;

            public float OutsideTemperature;
            public int CoolantTemperature;
        }

        public TemperatureStruct Temperatures;

        public bool AirCondition;
        public bool RearDemister;

        public int SolarSensor;
        public int FanSpeed;

        public ClimateHandler()
        {
            CanEventHandler.AddCanEventHandler(this);
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            switch(canFrame.CanID)
            {
                case CanID.INSIDE_TEMP_SOLAR:
                    Temperatures.InteriorTemperatureClimate = (canFrame.Data[3] / 10) + 6;
                    Temperatures.InteriorTemperatureDriverActual = (canFrame.Data[6] / 10) + 6;
                    Temperatures.InteriorTemperaturePassengerActual = (canFrame.Data[7] / 10) + 6;

                    SolarSensor = canFrame.Data[4];

                    Console.WriteLine($"IntClim: {Temperatures.InteriorTemperatureClimate}. Driv: {Temperatures.InteriorTemperatureDriverActual}. Pass: {Temperatures.InteriorTemperaturePassengerActual}");
                    break;

                case CanID.OUTSIDE_TEMP:
                    Temperatures.OutsideTemperature = (canFrame.Data[0] - 80f) / 2f;
                    break;

                case CanID.AIRCON_STATUS:
                    AirCondition = canFrame.Data[1] == CanValue.AIRCON_ON;
                    break;

                case CanID.CLIMATE_STATUS_DRIVER:
                    Temperatures.DriverTemperatureSetpoint = canFrame.Data[7] / 2;
                    FanSpeed = canFrame.Data[5];
                    break;

                case CanID.CLIMATE_STATUS_PASSENGER:
                    Temperatures.PassengerTemperatureSetpoint = canFrame.Data[7] / 2;
                    break;
            }
        }
    }
}
