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
    public class DoorHandler : ICanEvent
    {
        public enum DOOR_LOCK_STATUS
        {
            LOCKED,
            UNLOCKED,
            BEING_LOCKED,
            BEING_UNLOCKED
        }

        public enum DOOR_OPEN_STATUS
        {
            OPEN,
            CLOSED
        }

        public struct DoorStatus
        {
            public DOOR_OPEN_STATUS Driver;
            public DOOR_OPEN_STATUS Passenger;
            public DOOR_OPEN_STATUS DriverRear;
            public DOOR_OPEN_STATUS PassengerRear;

            public DOOR_OPEN_STATUS Trunk;
        }

        public DOOR_LOCK_STATUS CarLockStatus = DOOR_LOCK_STATUS.UNLOCKED;

        public DoorStatus Doors = new DoorStatus()
        {
            Driver = DOOR_OPEN_STATUS.CLOSED,
            Passenger = DOOR_OPEN_STATUS.CLOSED,
            DriverRear = DOOR_OPEN_STATUS.CLOSED,
            PassengerRear = DOOR_OPEN_STATUS.CLOSED,

            Trunk = DOOR_OPEN_STATUS.CLOSED
        };

        public DoorHandler()
        {
            CanEventHandler.AddCanEventHandler(this);
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            if (canFrame.CanID == CanID.DOOR_STATUS)
            {
                CheckLockStatus(canFrame);
                CheckDoorStatus(canFrame);
            }
        }

        private void CheckLockStatus(CanFrame canFrame)
        {
            switch(canFrame.Data[0])
            {
                case CanValue.LOCK_STATUS_LOCKED:
                    CarLockStatus = DOOR_LOCK_STATUS.LOCKED;
                    break;

                case CanValue.LOCK_STATUS_UNLOCKED:
                    CarLockStatus = DOOR_LOCK_STATUS.BEING_UNLOCKED;
                    break;

                case CanValue.LOCK_STATUS_BEEING_LOCKED:
                    CarLockStatus = DOOR_LOCK_STATUS.BEING_LOCKED;
                    break;

                case CanValue.LOCK_STATUS_BEEING_UNLOCKED:
                    CarLockStatus = DOOR_LOCK_STATUS.BEING_UNLOCKED;
                    break;
            }
        }

        private void CheckDoorStatus(CanFrame canFrame)
        {
            byte doorBits = canFrame.Data[1];

            switch(HelperClass.GetLSB(doorBits))
            {
                case 0x01:
                    Doors.Driver = DOOR_OPEN_STATUS.OPEN;
                    Doors.Passenger = DOOR_OPEN_STATUS.CLOSED;
                    break;

                case 0x04:
                    Doors.Driver = DOOR_OPEN_STATUS.CLOSED;
                    Doors.Passenger = DOOR_OPEN_STATUS.OPEN;
                    break;

                case 0x05:
                    Doors.Driver = DOOR_OPEN_STATUS.OPEN;
                    Doors.Passenger = DOOR_OPEN_STATUS.OPEN;
                    break;

                default:
                    Doors.Driver = DOOR_OPEN_STATUS.CLOSED;
                    Doors.Passenger = DOOR_OPEN_STATUS.CLOSED;
                    break;
            }

            switch (HelperClass.GetMSB(doorBits))
            {
                case 0x01:
                    Doors.DriverRear = DOOR_OPEN_STATUS.OPEN;
                    Doors.PassengerRear = DOOR_OPEN_STATUS.CLOSED;
                    break;

                case 0x04:
                    Doors.DriverRear = DOOR_OPEN_STATUS.CLOSED;
                    Doors.PassengerRear = DOOR_OPEN_STATUS.OPEN;
                    break;

                case 0x05:
                    Doors.DriverRear = DOOR_OPEN_STATUS.OPEN;
                    Doors.PassengerRear = DOOR_OPEN_STATUS.OPEN;
                    break;

                default:
                    Doors.DriverRear = DOOR_OPEN_STATUS.CLOSED;
                    Doors.PassengerRear = DOOR_OPEN_STATUS.CLOSED;
                    break;
            }

            if (canFrame.Data[2] == CanValue.DOOR_OPEN_TRUNK)
                Doors.Trunk = DOOR_OPEN_STATUS.OPEN;
            else
                Doors.Trunk = DOOR_OPEN_STATUS.CLOSED;
        }

        public void LockCar()
        {
            CanHandler.SendCanFrame(0x23A, new byte[] { 0x11, 0xCF, 0x01, 0xFF });
            CanHandler.SendCanFrame(0x23A, new byte[] { 0x11, 0xCF, 0x00, 0xFF });
            CanHandler.SendCanFrame(0x2B4, new byte[] { 0x00, 0xF1 });
        }

        public void UnlockCar()
        {
            CanHandler.SendCanFrame(0x23A, new byte[] { 0x11, 0xCF, 0x04, 0xFF });
            CanHandler.SendCanFrame(0x23A, new byte[] { 0x11, 0xCF, 0x00, 0xFF });
            CanHandler.SendCanFrame(0x2B4, new byte[] { 0x00, 0xF2 });
        }
    }
}
