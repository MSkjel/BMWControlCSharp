using BMWControl.CarHandlers;
using BMWControl.Handlers;
using BMWControl.Handlers.ButtonHandler;
using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.CanEvents
{
    public abstract class ICanEvent
    {
        public CanHandler CanHandler => BMWControl.CanHandler;
        public CanEventHandler CanEventHandler => BMWControl.CanEventHandler;
        public CarHandler CarHandler => BMWControl.CarHandler;
        public MultiMediaButtonHandler MultiMediaButtonHandler => CarHandler.MultiMediaButtonHandler;

        public virtual string Name => GetType().Name;

        public abstract void OnCanFrameReceived(CanFrame canFrame);
    }
}
