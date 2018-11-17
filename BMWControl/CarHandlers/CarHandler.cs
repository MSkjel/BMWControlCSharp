using BMWControl.CanEvents;
using BMWControl.Handlers;
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
        public static DoorHandler DoorHandler;
        public static LightHandler LightHandler;
        public static MultiMediaHandler MultiMediaHandler;
        public static SeatHandler SeatHandler;

        public int Mileage;
        public int Range;

        public float BatteryVoltage;
        public float TankLevel;
        public float AverageFuelUsage;

        public CarHandler()
        {
            CanEventHandler.AddCanEventHandler(this);

            DoorHandler = new DoorHandler();
            LightHandler = new LightHandler();
            MultiMediaHandler = new MultiMediaHandler();
            SeatHandler = new SeatHandler();
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            switch(canFrame.CanID)
            {
                case CanID.AVERAGE_SPEED_MILEAGE:
                    AverageFuelUsage = HelperClass.GetHexReversedValueFloat(new byte[] { (byte)HelperClass.GetMSB(canFrame.Data[1]), canFrame.Data[2] }) / 10;
                    Console.WriteLine(AverageFuelUsage);
                    break;
            }
        }
    }
}
