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

            MultiMediaButtonHandler.SteeringWheelUp.AddPressListener(new Action(() =>
            {
                PassengerSeatControls.SeatBackForward();
            }));

            MultiMediaButtonHandler.SteeringWheelDown.AddPressListener(new Action(() =>
            {
                PassengerSeatControls.SeatBackBackwards();
            }));

            MultiMediaButtonHandler.SteeringWheelDiamond.AddPressListener(new Action(() =>
            {
                PassengerSeatControls.SeatForward();
            }));

            MultiMediaButtonHandler.SteeringWheelStar.AddPressListener(new Action(() =>
            {
                PassengerSeatControls.SeatBackwards();
            }));

            MultiMediaButtonHandler.SteeringWheelPhone.AddReleaseListener(new Action(() =>
            {
                PassengerSeatControls.SeatUp();
            }));

            MultiMediaButtonHandler.SteeringWheelVoice.AddPressListener(new Action(() =>
            {
                PassengerSeatControls.SeatDown();
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
        private static CanHandler CanHandler => BMWControl.CanHandler;

        public static void SeatForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_FORWARD, 0x00, 0xC0, 0xFF });
        public static void SeatBackwards() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_BACKWARDS, 0x00, 0xC0, 0xFF });
        public static void SeatBackForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_BACK_FORWARD, 0x00, 0xC0, 0xFF });
        public static void SeatBackBackwards() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_BACK_BACKWARDS, 0x00, 0xC0, 0xFF });
        public static void SeatUp() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_UP, 0x00, 0xC0, 0xFF });
        public static void SeatDown() => CanHandler.SendCanFrame(CanID.SEAT_STATUS_DRIVER, new byte[] { CanValue.SEAT_DOWN, 0x00, 0xC0, 0xFF });

        public static void PressMButton() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_DRIVER, new byte[] { CanValue.SEAT_M_BUTTON, 0xFF });
        public static void Press1Button() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_DRIVER, new byte[] { CanValue.SEAT_1_BUTTON, 0xFF });
        public static void Press2Button() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_DRIVER, new byte[] { CanValue.SEAT_2_BUTTON, 0xFF });
    }

    public class PassengerSeatControls
    {
        private static CanHandler CanHandler => BMWControl.CanHandler;

        public static void SeatForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_FORWARD, 0x00, 0xC0, 0xFF });
        public static void SeatBackwards() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_BACKWARDS, 0x00, 0xC0, 0xFF });
        public static void SeatBackForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_BACK_FORWARD, 0x00, 0xC0, 0xFF });
        public static void SeatBackBackwards() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_BACK_BACKWARDS, 0x00, 0xC0, 0xFF });
        public static void SeatUp() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_UP, 0x00, 0xC0, 0xFF });
        public static void SeatDown() => CanHandler.SendCanFrame(CanID.SEAT_STATUS_PASSENGER, new byte[] { CanValue.SEAT_DOWN, 0x00, 0xC0, 0xFF });

        public static void PressMButton() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_PASSENGER, new byte[] { CanValue.SEAT_M_BUTTON, 0xFF });
        public static void Press1Button() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_PASSENGER, new byte[] { CanValue.SEAT_1_BUTTON, 0xFF });
        public static void Press2Button() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_PASSENGER, new byte[] { CanValue.SEAT_2_BUTTON, 0xFF });
    }
}
