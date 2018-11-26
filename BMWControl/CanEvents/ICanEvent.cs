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
        protected ConfigHandler ConfigHandler => BMWControl.ConfigHandler;
        protected CanHandler CanHandler => BMWControl.CanHandler;
        protected CanEventHandler CanEventHandler => BMWControl.CanEventHandler;
        protected CarHandler CarHandler => BMWControl.CarHandler;
        protected MultiMediaButtonHandler MultiMediaButtonHandler => CarHandler.MultiMediaButtonHandler;

        public virtual string Name => GetType().Name;

        public abstract void OnCanFrameReceived(CanFrame canFrame);
    }
}
