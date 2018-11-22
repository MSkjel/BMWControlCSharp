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
    public class LightHandler : ICanEvent
    {
        public LightHandler()
        {
            CanEventHandler.AddCanEventHandler(this);
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            
        }
    }
}
