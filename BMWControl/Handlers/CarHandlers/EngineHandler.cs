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
            public float Coolant;
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
        public int Throttle;
        public int RPM;

        public float Torque;
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
                    Temperatures.Coolant = canFrame.Data[0] - 48f;
                    //OilPressure?
                    //Handbrake?
                    break;

                case CanID.IGNITION_STATUS:
                    Ignition = (IgnitionStatus)canFrame.Data[0];
                    break;

                case CanID.AVERAGE_SPEED_MILEAGE:
                    AverageFuelUsage = HelperClass.GetHexReversedValueFloat(new byte[] { HelperClass.GetMSB(canFrame.Data[1]), canFrame.Data[2] }) / 10;
                    break;

                case CanID.ENGINE_RPM_THROTTLE:
                    RPM = HelperClass.GetHexReversedValueInt(new byte[] { canFrame.Data[4], canFrame.Data[5]}) / 4;
                    Throttle = HelperClass.GetHexReversedValueInt(new byte[] { canFrame.Data[2], canFrame.Data[3] });
                    break;

                case CanID.TORQUE_BRAKE:
                    Torque = HelperClass.GetHexReversedValueFloat(new byte[] { canFrame.Data[1], (byte)(canFrame.Data[2] * 256) }) / 32;
                    break;
            }
        }
    }
}
