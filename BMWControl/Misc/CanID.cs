using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.Misc
{
    public class CanValue
    {
        //Lights
        public const int INSIDE_LIGHTS_ON = 0xF5;

        //Lock status
        public const int LOCK_STATUS_LOCKED = 0x84;
        public const int LOCK_STATUS_UNLOCKED = 0x81;
        public const int LOCK_STATUS_BEEING_LOCKED = 0x31;
        public const int LOCK_STATUS_BEEING_UNLOCKED = 0x10;

        //Door status
        public const int DOOR_OPEN_TRUNK = 0x01;

        //Ignition status
        public const int IGNITION_STATUS_OFF = 0x00;
        public const int IGNITION_STATUS_ACC = 0x41;
        public const int IGNITION_STATUS_ON = 0x45;
        public const int IGNITION_STATUS_ENGINE_START = 0x55;

    }

    public class CanID
    {
        //RPM
        public const int ENGINE_RPM = 0xAA;

        //Speeds
        public const int INDIVIDUAL_WHEEL_SPEEDS = 0xCE;
        public const int VEHICLE_SPEED = 0x1A6;

        //Fuel Range and Mileage
        public const int FUEL_MIL_RNG = 0x330;

        //Average speed and Mileage
        public const int AVERAGE_SPEED_MILEAGE = 0x362;

        //VIN
        public const int VIN_NUMBER = 0x380;

        //Inside lights
        public const int INSIDE_LIGHTS = 0x2F6;

        //Door status
        public const int DOOR_STATUS = 0x2FC;

        //Temperature
        public const int OUTSIDE_TEMP = 0x2CA;
        public const int ENGINE_TEMP = 0x1D0;
        public const int INSIDE_TEMP = 0x32E;


        //Ignition status
        public const int IGNITION_STATUS = 0x130;

        //Voltage
        public const int BATTERY_VOLTAGE = 0x3B4;

        //Seat
        public const int SEAT_STATUS_DRIVER = 0x232;
        public const int SEAT_STATUS_PASSENGER = 0x22A;
    }
}
