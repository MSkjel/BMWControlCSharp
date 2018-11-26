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
        public const int LOCK_STATUS_BEING_LOCKED = 0x31;
        public const int LOCK_STATUS_BEING_UNLOCKED = 0x10;

        //Door status
        public const int DOOR_OPEN_TRUNK = 0x01;

        //Ignition status
        public const int IGNITION_STATUS_OFF = 0x00;
        public const int IGNITION_STATUS_ACC = 0x41;
        public const int IGNITION_STATUS_ON = 0x45;
        public const int IGNITION_STATUS_ENGINE_START = 0x55;

        //Steering wheel buttons
        public const int STEERING_WHEEL_BUTTON_VOL_DOWN = 0xC4;
        public const int STEERING_WHEEL_BUTTON_VOL_UP = 0xC8;
        public const int STEERING_WHEEL_BUTTON_VOICE = 0xC1;
        public const int STEERING_WHEEL_BUTTON_PHONE = 0x01;
        public const int STEERING_WHEEL_BUTTON_PHONE_DIAMOND_STAR_HORN = 0xC0;
        public const int STEERING_WHEEL_BUTTON_DIAMOND = 0x40;
        public const int STEERING_WHEEL_BUTTON_STAR = 0x10;
        public const int STEERING_WHEEL_BUTTON_UP = 0xE0;
        public const int STEERING_WHEEL_BUTTON_DOWN = 0xD0;
        public const int STEERING_WHEEL_BUTTON_HORN = 0x04;

        //iDrive Controller
        public const int IDRIVE_CONTROLLER_MENU_PRESS = 0x0F;
        public const int IDRIVE_CONTROLLER_MENU = 0xC4;
        public const int IDRIVE_CONTROLLER_PRESS = 0xC1;
        public const int IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT = 0xC0;
        public const int IDRIVE_CONTROLLER_UP = 0x00;
        public const int IDRIVE_CONTROLLER_DOWN = 0x04;
        public const int IDRIVE_CONTROLLER_LEFT = 0x06;
        public const int IDRIVE_CONTROLLER_RIGHT = 0x02;

        //Seat Controls
        public const int SEAT_FORWARD = 0x01;
        public const int SEAT_BACKWARDS = 0x02;
        public const int SEAT_UP = 0x04;
        public const int SEAT_DOWN = 0x08;
        public const int SEAT_BACK_FORWARD = 0x10;
        public const int SEAT_BACK_BACKWARDS = 0x20;
        public const int SEAT_TILT_FORWARDS = 0x80;
        public const int SEAT_TILT_BACKWARDS = 0x40;
        public const int SEAT_CUSHION_FORWARD = 0x1C;
        public const int SEAT_CUSHION_BACKWARDS = 0x2C;
        public const int SEAT_HEADREST_UP = 0xC1;
        public const int SEAT_HEADREST_DOWN = 0xC2;
        public const int SEAT_M_BUTTONS_IDLE = 0xF8;
        public const int SEAT_M_BUTTON = 0xFC;
        public const int SEAT_1_BUTTON = 0xF9;
        public const int SEAT_2_BUTTON = 0xFA;
    }

    public class CanID
    {
        //RPM
        public const int ENGINE_RPM_THROTTLE = 0xAA;

        //Speeds
        public const int INDIVIDUAL_WHEEL_SPEEDS = 0xCE;
        public const int VEHICLE_SPEED = 0x1A6;

        //Fuel Range and Mileage
        public const int FUEL_ODO_RNG = 0x330;

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
        public const int INSIDE_TEMP = 0x32E;


        //Engine stuff
        public const int IGNITION_STATUS = 0x130;
        public const int ENGINE_TEMP_PRESSURE_HANDBRAKE = 0x1D0;
        public const int TORQUE_BRAKE = 0xA8;

        //Voltage
        public const int BATTERY_VOLTAGE = 0x3B4;

        //Seat
        public const int SEAT_STATUS_DRIVER = 0x232;
        public const int SEAT_STATUS_PASSENGER = 0x22A;

        //Steering wheel
        public const int STEERING_WHEEL_CONTROLS = 0x1D6;
        public const int STEERING_WHEEL_ANGLE = 0x0C8;

        //iDrive Controller
        public const int IDRIVE_CONTROLLER = 0x1B8;

        //Seat Controls Passenger
        public const int SEAT_CONTROLS_ADJUSTMENT_PASSENGER = 0xD2;
        public const int SEAT_CONTROLS_MEMORY_BUTTONS_PASSENGER = 0x1F2;

        //Seat Controls Driver
        public const int SEAT_CONTROLS_ADJUSTMENT_DRIVER = 0xDA;
        public const int SEAT_CONTROLS_MEMORY_BUTTONS_DRIVER = 0x1F3;

        //Time
        public const int SET_DATE_TIME = 0x39E;
    }
}
