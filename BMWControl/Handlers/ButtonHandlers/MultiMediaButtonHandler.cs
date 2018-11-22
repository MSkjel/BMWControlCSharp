using BMWControl.CanEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMWControl.Misc;

namespace BMWControl.Handlers.ButtonHandler
{
    public class MultiMediaButtonHandler
    {
        #region Steering Wheel Buttons
        public List<IButtonEvent> SteeringWheelButtons = new List<IButtonEvent>();

        public IButtonEvent SteeringWheelVolUp = new IButtonEvent("Steering_Wheel_Up", CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_VOL_UP, 0x00 });
        public IButtonEvent SteeringWheelVolDown = new IButtonEvent("Steering_Wheel_Down", CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_VOL_DOWN, 0x00 });
        public IButtonEvent SteeringWheelVoice = new IButtonEvent("Steering_Wheel_Voice", CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_VOICE, 0x01 });
        public IButtonEvent SteeringWheelPhone = new IButtonEvent("Steering_Wheel_Phone", CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_PHONE, 0x00 });
        public IButtonEvent SteeringWheelDiamond = new IButtonEvent("Steering_Wheel_Diamond", CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_DIAMOND_STAR_HORN, CanValue.STEERING_WHEEL_BUTTON_DIAMOND });
        public IButtonEvent SteeringWheelStar = new IButtonEvent("Steering_Wheel_Star", CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_DIAMOND_STAR_HORN, CanValue.STEERING_WHEEL_BUTTON_STAR });
        public IButtonEvent SteeringWheelUp = new IButtonEvent("Steering_Wheel_Up", CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_UP, 0x00 });
        public IButtonEvent SteeringWheelDown = new IButtonEvent("Steering_Wheel_Down", CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_DOWN, 0x00 });
        public IButtonEvent SteeringWheelHorn = new IButtonEvent("Steering_Wheel_Horn");
        #endregion

        #region iDrive Buttons
        public List<IButtonEvent> iDriveButtons = new List<IButtonEvent>();

        public IButtonEvent iDriveMenu = new IButtonEvent("iDriveMenu");
        public IButtonEvent iDrivePress = new IButtonEvent("iDrivePress");
        public IButtonEvent iDriveUp = new IButtonEvent("iDriveUp");
        public IButtonEvent iDriveDown = new IButtonEvent("iDriveDown");
        public IButtonEvent iDriveLeft = new IButtonEvent("iDriveLeft");
        public IButtonEvent iDriveRight = new IButtonEvent("iDriveRight");
        #endregion

        public MultiMediaButtonHandler()
        {
            SteeringWheelButtons.AddRange(new List<IButtonEvent>()
            {
                SteeringWheelVolUp,
                SteeringWheelVolDown,
                SteeringWheelVoice,
                SteeringWheelPhone,
                SteeringWheelDiamond,
                SteeringWheelStar,
                SteeringWheelUp,
                SteeringWheelDown,
                SteeringWheelHorn
            });

            iDriveButtons.AddRange(new List<IButtonEvent>()
            {
                iDriveMenu,
                iDrivePress,
                iDriveUp,
                iDriveDown,
                iDriveLeft,
                iDriveRight
            });
        }
    }
}
