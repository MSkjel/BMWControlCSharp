using BMWControl.CanEvents;
using BMWControl.Handlers;
using BMWControl.Handlers.ButtonHandler;
using BMWControl.Handlers.CarHandlers;
using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.CarHandlers
{
    public class CarHandler : ICanEvent
    {
        #region CanHandlers
        public static DoorHandler DoorHandler;
        public static LightHandler LightHandler;
        public static MultiMediaHandler MultiMediaHandler;
        public static SeatHandler SeatHandler;
        public static SpeedHandler SpeedHandler;
        public static EngineHandler EngineHandler;
        public static TimeHandler TimeHandler;
        public static ClimateHandler ClimateHandler;
        #endregion

        #region Buttons
        public new static MultiMediaButtonHandler MultiMediaButtonHandler = new MultiMediaButtonHandler();
        #endregion

        public string NiceName = "Test1";
        public string VIN = "Test1";

        public DateTime CarTime = DateTime.Now;

        public int Odometer;
        public int Range;
        public int SteeringWheelAngle;

        public float BatteryVoltage;
        public float TankLevel;

        public CarHandler()
        {
            CanEventHandler.AddCanEventHandler(this);

            DoorHandler = new DoorHandler();
            LightHandler = new LightHandler();
            MultiMediaHandler = new MultiMediaHandler();
            SeatHandler = new SeatHandler();
            SpeedHandler = new SpeedHandler();
            EngineHandler = new EngineHandler();
            TimeHandler = new TimeHandler();
            ClimateHandler = new ClimateHandler();
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            switch (canFrame.CanID)
            {
                case CanID.FUEL_ODO_RNG:
                    Odometer = HelperClass.GetHexReversedValueInt(new byte[] { canFrame.Data[0], canFrame.Data[1], canFrame.Data[2] });
                    Range = HelperClass.GetHexReversedValueInt(new byte[] { canFrame.Data[6], canFrame.Data[7]}) / 16;
                    TankLevel = canFrame.Data[3];
                    break;

                case CanID.BATTERY_VOLTAGE:
                    BatteryVoltage = (HelperClass.GetHexReversedValueFloat(new byte[] { canFrame.Data[0], canFrame.Data[1] }) - 61440f) / 68;
                    break;

                case CanID.STEERING_WHEEL_ANGLE:
                    int raw = HelperClass.GetHexReversedValueInt(new byte[] { canFrame.Data[0], canFrame.Data[1] });

                    if (raw > 10000)
                        SteeringWheelAngle = (raw - 65535) / 23;
                    else
                        SteeringWheelAngle = raw / 23;
                    break;

                case CanID.VIN_NUMBER:
                    VIN = HelperClass.DecodeVIN(canFrame.Data);
                    break;
            }
        }
    }
}
