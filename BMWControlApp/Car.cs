using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Environment = System.Environment;

namespace BMWControlApp
{
    public class Car
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

        public DoorStatus Doors = new DoorStatus()
        {
            Driver = DOOR_OPEN_STATUS.CLOSED,
            Passenger = DOOR_OPEN_STATUS.CLOSED,
            DriverRear = DOOR_OPEN_STATUS.CLOSED,
            PassengerRear = DOOR_OPEN_STATUS.CLOSED,

            Trunk = DOOR_OPEN_STATUS.CLOSED
        };

        public struct TemperatureStruct
        {
            public int Coolant;
            public int Oil;
            public int Intake;
        }

        public enum IgnitionStatus
        {
            OFF = 0x00,
            ACC = 0x41,
            ON = 0x45,
            STARTING = 0x55
        }

        public struct SpeedStruct
        {
            public float FrontLeft;
            public float FrontRight;
            public float RearLeft;
            public float RearRight;

            public float Vehicle;
            public float Average;
        }

        public DateTime CarTime;
        public DateTime LastUpdate;

        public string VIN;
        public string NiceName;

        public int Odometer;
        public int Range;
        public int SteeringWheelAngle;

        public float BatteryVoltage;
        public float TankLevel;

        public DOOR_LOCK_STATUS CarLockStatus = DOOR_LOCK_STATUS.UNLOCKED;

        public int OilPressure;
        public int Throttle;
        public int RPM;

        public float Torque;
        public float AverageFuelUsage;

        public TemperatureStruct Temperatures = new TemperatureStruct();

        public IgnitionStatus Ignition = IgnitionStatus.OFF;

        public SpeedStruct Speeds = new SpeedStruct();

        public override string ToString()
        {
            try
            {
                return
                    "Last Update: " + (int)(DateTime.Now - LastUpdate).TotalSeconds + " seconds ago" + Environment.NewLine + Environment.NewLine +
                    "VIN: " + VIN + Environment.NewLine +
                    "Odometer: " + Odometer + Environment.NewLine +
                    //"Inside Lights: " + InsideLights + Environment.NewLine +
                    "Locked: " + CarLockStatus + Environment.NewLine +
                    "Ignition: " + Ignition + Environment.NewLine +
                    "Tank Level: " + TankLevel + Environment.NewLine +
                    "Range: " + Range + Environment.NewLine +
                    "Battery Voltage: " + BatteryVoltage + Environment.NewLine +
                    //"Temperature Inside: " + Temperatures.Inside + Environment.NewLine +
                    //"Temperature Outside: " + Temperatures.Outside + Environment.NewLine +
                    "Temperature Engine: " + Temperatures.Coolant + Environment.NewLine +
                    "Driver Door Open: " + Doors.Driver + Environment.NewLine +
                    "Passenger Door Open: " + Doors.Passenger + Environment.NewLine +
                    "Rear Driver Door Open: " + Doors.DriverRear + Environment.NewLine +
                    "Rear Passenger Door Open: " + Doors.PassengerRear + Environment.NewLine +
                    "Trunk Open: " + Doors.Trunk;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return base.ToString();
            }
        }
    }
}