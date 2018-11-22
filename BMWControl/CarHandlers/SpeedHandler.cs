using BMWControl.CanEvents;
using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.CarHandlers
{
    public class SpeedHandler : ICanEvent
    {
        public struct SpeedStruct
        {
            public float FrontLeft;
            public float FrontRight;
            public float RearLeft;
            public float RearRight;

            public float Vehicle;
            public float Average;
        }

        public SpeedStruct Speeds = new SpeedStruct();

        public SpeedHandler()
        {
            CanEventHandler.AddCanEventHandler(this);
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            switch(canFrame.CanID)
            {
                case CanID.INDIVIDUAL_WHEEL_SPEEDS:
                    Speeds.FrontLeft = HelperClass.GetHexReversedValueFloat(canFrame.Data.Take(2));
                    Speeds.FrontRight = HelperClass.GetHexReversedValueFloat(canFrame.Data.Skip(2).Take(2));
                    Speeds.RearLeft = HelperClass.GetHexReversedValueFloat(canFrame.Data.Skip(4).Take(2));
                    Speeds.RearRight = HelperClass.GetHexReversedValueFloat(canFrame.Data.Skip(6).Take(2));
                    break;

                case CanID.VEHICLE_SPEED:
                    Speeds.Vehicle = HelperClass.GetHexReversedValueFloat(canFrame.Data.Take(2)) / 10;
                    break;

                case CanID.AVERAGE_SPEED_MILEAGE:
                    Speeds.Average = HelperClass.GetHexReversedValueFloat(new byte[] { canFrame.Data[0], HelperClass.GetLSB(canFrame.Data[1]) }) / 10;
                    break;
            }
        }


        public override string ToString()
        {
            return $"FL: {Speeds.FrontLeft}. FR: {Speeds.FrontRight}. RL: {Speeds.RearLeft}. RR: {Speeds.RearRight}. Vehicle: {Speeds.Vehicle}. Average: {Speeds.Average}";
        }
    }
}
