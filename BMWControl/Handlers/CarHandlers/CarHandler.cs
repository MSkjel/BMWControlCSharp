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
            //switch(canFrame.CanID)
            //{
                
            //}
        }
    }
}
