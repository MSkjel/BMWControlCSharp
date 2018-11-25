using BMWControl.CanEvents;
using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.CarHandlers
{
    public class EngineHandler : ICanEvent
    {
        public struct TemperatureStruct
        {
            public int Coolant;
            public int Oil;
            public int Intake;
        }

        public enum IgnitionStatus
        {
            OFF = 0x00,
            ACC = 0x41,
            ON = 0x45,
            STARTING = 0x55
        }

        public int OilPressure;
        public int Torque;
        public int Throttle;
        public int RPM;

        public float AverageFuelUsage;

        public TemperatureStruct Temperatures = new TemperatureStruct();

        public IgnitionStatus Ignition = IgnitionStatus.OFF;

        public EngineHandler()
        {
            CanEventHandler.AddCanEventHandler(this);
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            switch(canFrame.CanID)
            {
                case CanID.ENGINE_TEMP_PRESSURE_HANDBRAKE:
                    Temperatures.Coolant = canFrame.Data[0] - 48;
                    //OilPressure?
                    //Handbrake?
                    break;

                case CanID.IGNITION_STATUS:
                    Ignition = (IgnitionStatus)canFrame.Data[0];
                    break;

                case CanID.AVERAGE_SPEED_MILEAGE:
                    AverageFuelUsage = HelperClass.GetHexReversedValueFloat(new byte[] { HelperClass.GetMSB(canFrame.Data[1]), canFrame.Data[2] }) / 10;
                    break;

                case CanID.ENGINE_RPM:
                    RPM = HelperClass.GetHexReversedValueInt(new byte[] { canFrame.Data[4], canFrame.Data[5]}) / 4;
                    break;
            }
        }
    }
}
