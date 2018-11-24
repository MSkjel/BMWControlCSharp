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
                PassengerSeatControls.TiltForward();
            }));

            MultiMediaButtonHandler.SteeringWheelDown.AddPressListener(new Action(() =>
            {
                PassengerSeatControls.TiltBackward();
            }));

            MultiMediaButtonHandler.SteeringWheelDiamond.AddPressListener(new Action(() =>
            {
                PassengerSeatControls.CushionForward();
            }));

            MultiMediaButtonHandler.SteeringWheelStar.AddPressListener(new Action(() =>
            {
                PassengerSeatControls.CushionBackward();
            }));

            MultiMediaButtonHandler.SteeringWheelPhone.AddPressListener(new Action(() =>
            {
                PassengerSeatControls.HeadrestUp();
            }));

            MultiMediaButtonHandler.SteeringWheelVoice.AddPressListener(new Action(() =>
            {
                PassengerSeatControls.HeadrestDown();
            }));
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            CheckSeatHeatingLevel(canFrame);

            if(canFrame.CanID == CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER)
                Console.WriteLine("Adjustment: " + canFrame.ToString());
            else if(canFrame.CanID == CanID.SEAT_CONTROLS_MEMORY_BUTTONS_DRIVER)
                Console.WriteLine("Mem buttons: " + canFrame.ToString());
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

        public static void Forward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_FORWARD, 0x00, 0xC0, 0xFF });
        public static void Backwards() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_BACKWARDS, 0x00, 0xC0, 0xFF });
        public static void BackrestForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_BACK_FORWARD, 0x00, 0xC0, 0xFF });
        public static void BackrestBackwards() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_BACK_BACKWARDS, 0x00, 0xC0, 0xFF });
        public static void Up() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_UP, 0x00, 0xC0, 0xFF });
        public static void Down() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_DOWN, 0x00, 0xC0, 0xFF });
        public static void TiltForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_TILT_FORWARDS, 0x00, 0xC0, 0xFF });
        public static void TiltBackward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { CanValue.SEAT_TILT_BACKWARDS, 0x00, 0xC0, 0xFF });
        public static void CushionForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { 0x00, CanValue.SEAT_CUSHION_FORWARD, 0xC0, 0xFF });
        public static void CushionBackward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { 0x00, CanValue.SEAT_CUSHION_BACKWARDS, 0xC0, 0xFF });
        public static void HeadrestUp() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { 0x00, 0x00, CanValue.SEAT_HEADREST_UP, 0xFF });
        public static void HeadrestDown() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_DRIVER, new byte[] { 0x00, 0x00, CanValue.SEAT_HEADREST_DOWN, 0xFF });

        public static void MButton() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_DRIVER, new byte[] { CanValue.SEAT_M_BUTTON, 0xFF });
        public static void Mem1Button() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_DRIVER, new byte[] { CanValue.SEAT_1_BUTTON, 0xFF });
        public static void Mem2Button() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_DRIVER, new byte[] { CanValue.SEAT_2_BUTTON, 0xFF });
    }

    public class PassengerSeatControls
    {
        private static CanHandler CanHandler => BMWControl.CanHandler;

        public static void Forward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_FORWARD, 0x00, 0xC0, 0xFF });
        public static void Backwards() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_BACKWARDS, 0x00, 0xC0, 0xFF });
        public static void BackrestForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_BACK_FORWARD, 0x00, 0xC0, 0xFF });
        public static void BackrestBackwards() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_BACK_BACKWARDS, 0x00, 0xC0, 0xFF });
        public static void Up() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_UP, 0x00, 0xC0, 0xFF });
        public static void Down() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_DOWN, 0x00, 0xC0, 0xFF });
        public static void TiltForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_TILT_FORWARDS, 0x00, 0xC0, 0xFF });
        public static void TiltBackward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { CanValue.SEAT_TILT_BACKWARDS, 0x00, 0xC0, 0xFF });
        public static void CushionForward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { 0x00, CanValue.SEAT_CUSHION_FORWARD, 0xC0, 0xFF });
        public static void CushionBackward() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { 0x00, CanValue.SEAT_CUSHION_BACKWARDS, 0xC0, 0xFF });
        public static void HeadrestUp() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { 0x00, 0x00, CanValue.SEAT_HEADREST_UP, 0xFF });
        public static void HeadrestDown() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_ADJUSTMENT_PASSENGER, new byte[] { 0x00, 0x00, CanValue.SEAT_HEADREST_DOWN, 0xFF });

        public static void MButton() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_PASSENGER, new byte[] { CanValue.SEAT_M_BUTTON, 0xFF });
        public static void Mem1Button() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_PASSENGER, new byte[] { CanValue.SEAT_1_BUTTON, 0xFF });
        public static void Mem2Button() => CanHandler.SendCanFrame(CanID.SEAT_CONTROLS_MEMORY_BUTTONS_PASSENGER, new byte[] { CanValue.SEAT_2_BUTTON, 0xFF });
    }
}
