using BMWControl.CanEvents;
using BMWControl.Handlers;
using BMWControl.Handlers.ButtonHandler;
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
        #endregion

        #region Buttons
        public new static MultiMediaButtonHandler MultiMediaButtonHandler = new MultiMediaButtonHandler();
        #endregion

        public string NiceName = "Test1";

        public int Mileage;
        public int Range;

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
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            switch (canFrame.CanID)
            {
                case CanID.FUEL_MIL_RNG:
                    Mileage = canFrame.Data[2] + canFrame.Data[1] + canFrame.Data[0];
                    Range = HelperClass.GetHexReversedValueInt(new byte[] { canFrame.Data[6], canFrame.Data[7]}) / 16;
                    TankLevel = canFrame.Data[3];
                    break;

                case CanID.BATTERY_VOLTAGE:
                    BatteryVoltage = (HelperClass.GetHexReversedValueFloat(new byte[] { canFrame.Data[0], canFrame.Data[1] }) - 61440f) / 68;
                    break;
            }
        }
    }
}
