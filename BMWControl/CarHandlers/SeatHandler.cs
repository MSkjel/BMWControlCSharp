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
    public class SeatHandler : ICanEvent
    {
        public SeatHeatingStruct SeatHeating = new SeatHeatingStruct();

        public struct SeatHeatingStruct
        {
            public SeatHeatingLevel Driver;
            public SeatHeatingLevel Passenger;
        }

        public enum SeatHeatingLevel
        {
            OFF = 0,
            LOW = 1,
            MEDIUM = 2,
            HIGH = 3
        }

        public SeatHandler()
        {
            CanEventHandler.AddCanEventHandler(this);

            SteeringWheelButtons.OnUpPressed.Add(new Action(() =>
            {
                PassengerSeatControls.SeatBackForward();
            }));

            SteeringWheelButtons.OnDownPressed.Add(new Action(() =>
            {
                PassengerSeatControls.SeatBackBackwards();
            }));
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            CheckSeatHeatingLevel(canFrame);
        }

        public void CheckSeatHeatingLevel(CanFrame canFrame)
        {
            if (canFrame.CanID == CanID.SEAT_STATUS_DRIVER)
            {
                SeatHeating.Driver = (SeatHeatingLevel)HelperClass.GetMSB(canFrame.Data[0]);
            }
            else if (canFrame.CanID == CanID.SEAT_STATUS_PASSENGER)
            {
                SeatHeating.Passenger = (SeatHeatingLevel)HelperClass.GetMSB(canFrame.Data[0]);
            }
        }

        public void SetSeatHeatingLevelDriver(SeatHeatingLevel seatHeatingLevel)
        {
            int presses = 0;
        }
    }

    public class DriverSeatControls
    {

    }

    public class PassengerSeatControls
    {
        private static CanHandler CanHandler => BMWControl.CanHandler;

        public static void SeatBackForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_PASSENGER, new byte[] { CanValue.SEAT_BACK_FORWARD, 0x00, 0xC0, 0xFF });
        public static void SeatBackBackwards() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_PASSENGER, new byte[] { CanValue.SEAT_BACK_BACKWARDS, 0x00, 0xC0, 0xFF });
    }
}
