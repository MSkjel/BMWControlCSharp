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

        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            
        }
    }
}
