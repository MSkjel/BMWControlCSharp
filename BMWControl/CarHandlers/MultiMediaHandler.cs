using BMWControl.CanEvents;
using BMWControl.Handlers;
using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BMWControl.CarHandlers
{
    public class MultiMediaHandler : ICanEvent
    {
        public MultiMediaHandler()
        {
            CanEventHandler.AddCanEventHandler(this);          
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            switch(canFrame.CanID)
            {
                case CanID.STEERING_WHEEL_CONTROLS:
                    //Console.WriteLine("Got Wheel input");
                    switch(canFrame.Data[0])
                    {
                        case CanValue.STEERING_WHEEL_BUTTON_VOL_UP:
                            SteeringWheelButtons.OnVolUpPressed.ForEach(x => x.Invoke());
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_VOL_DOWN:
                            SteeringWheelButtons.OnVolDownPressed.ForEach(x => x.Invoke());
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_VOICE:
                            SteeringWheelButtons.OnVoicePressed.ForEach(x => x.Invoke());
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_PHONE:
                            SteeringWheelButtons.OnPhonePressed.ForEach(x => x.Invoke());
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_DIAMOND_STAR_HORN:
                            if (canFrame.Data[2] == CanValue.STEERING_WHEEL_BUTTON_DIAMOND)
                                SteeringWheelButtons.OnDiamondPressed.ForEach(x => x.Invoke());
                            else if (canFrame.Data[2] == CanValue.STEERING_WHEEL_BUTTON_STAR)
                                SteeringWheelButtons.OnStarPressed.ForEach(x => x.Invoke());
                            else if (canFrame.Data[2] == CanValue.STEERING_WHEEL_BUTTON_HORN)
                                SteeringWheelButtons.OnHornPressed.ForEach(x => x.Invoke());
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_UP:
                            SteeringWheelButtons.OnUpPressed.ForEach(x => x.Invoke());
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_DOWN:
                            SteeringWheelButtons.OnDownPressed.ForEach(x => x.Invoke());
                            break;

                        default:
                            if(SteeringWheelButtons.VolUpPressed)
                            {
                                SteeringWheelButtons.OnVolUpReleased.ForEach(x => x.Invoke());
                                SteeringWheelButtons.VolUpPressed = false;
                            }
                            else if (SteeringWheelButtons.VolDownPressed)
                            {
                                SteeringWheelButtons.OnVolDownReleased.ForEach(x => x.Invoke());
                                SteeringWheelButtons.VolDownPressed = false;
                            }
                            else if (SteeringWheelButtons.VoicePressed)
                            {
                                SteeringWheelButtons.OnVoiceReleased.ForEach(x => x.Invoke());
                                SteeringWheelButtons.VoicePressed = false;
                            }
                            else if (SteeringWheelButtons.PhonePressed)
                            {
                                SteeringWheelButtons.OnPhoneReleased.ForEach(x => x.Invoke());
                                SteeringWheelButtons.PhonePressed = false;
                            }
                            else if (SteeringWheelButtons.DiamondPressed)
                            {
                                SteeringWheelButtons.OnDiamondReleased.ForEach(x => x.Invoke());
                                SteeringWheelButtons.DiamondPressed = false;
                            }
                            else if (SteeringWheelButtons.StarPressed)
                            {
                                SteeringWheelButtons.OnStarReleased.ForEach(x => x.Invoke());
                                SteeringWheelButtons.StarPressed = false;
                            }
                            else if (SteeringWheelButtons.UpPressed)
                            {
                                SteeringWheelButtons.OnUpReleased.ForEach(x => x.Invoke());
                                SteeringWheelButtons.UpPressed = false;
                            }
                            else if (SteeringWheelButtons.HornPressed)
                            {
                                SteeringWheelButtons.OnHornReleased.ForEach(x => x.Invoke());
                                SteeringWheelButtons.HornPressed = false;
                            }
                            break;
                    }

                    break;

                case CanID.IDRIVE_CONTROLLER:
                    Console.WriteLine(canFrame.ToString());
                    switch (canFrame.Data[0])
                    {
                        case CanValue.IDRIVE_CONTROLLER_MENU_PRESS:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_MENU)
                                iDriveController.OnMenuPressed.ForEach(x => x.Invoke());
                            else if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_PRESS)
                                iDriveController.OnPress.ForEach(x => x.Invoke());
                            break;

                        case CanValue.IDRIVE_CONTROLLER_UP:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                iDriveController.OnUp.ForEach(x => x.Invoke());
                            break;

                        case CanValue.IDRIVE_CONTROLLER_DOWN:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                iDriveController.OnDown.ForEach(x => x.Invoke());
                            break;

                        case CanValue.IDRIVE_CONTROLLER_LEFT:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                iDriveController.OnLeft.ForEach(x => x.Invoke());
                            break;

                        case CanValue.IDRIVE_CONTROLLER_RIGHT:
                            if(canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                iDriveController.OnRight.ForEach(x => x.Invoke());
                            break;
                    }

                break;
            }
        }
    }

    public class SteeringWheelButtons
    {
        private static CanHandler CanHandler => BMWControl.CanHandler;

        public static bool VolUpPressed = false;
        public static bool VolDownPressed = false;
        public static bool VoicePressed = false;
        public static bool PhonePressed = false;
        public static bool DiamondPressed = false;
        public static bool StarPressed = false;
        public static bool UpPressed = false;
        public static bool DownPressed = false;
        public static bool HornPressed = false;

        public static List<Action> OnVolUpPressed = new List<Action>();
        public static List<Action> OnVolDownPressed = new List<Action>();
        public static List<Action> OnVoicePressed = new List<Action>();
        public static List<Action> OnPhonePressed = new List<Action>();
        public static List<Action> OnDiamondPressed = new List<Action>();
        public static List<Action> OnStarPressed = new List<Action>();
        public static List<Action> OnUpPressed = new List<Action>();
        public static List<Action> OnDownPressed = new List<Action>();
        public static List<Action> OnHornPressed = new List<Action>();

        public static List<Action> OnVolUpReleased = new List<Action>();
        public static List<Action> OnVolDownReleased = new List<Action>();
        public static List<Action> OnVoiceReleased = new List<Action>();
        public static List<Action> OnPhoneReleased = new List<Action>();
        public static List<Action> OnDiamondReleased = new List<Action>();
        public static List<Action> OnStarReleased = new List<Action>();
        public static List<Action> OnUpReleased = new List<Action>();
        public static List<Action> OnDownReleased = new List<Action>();
        public static List<Action> OnHornReleased = new List<Action>();

        public static void PressVolUp() => CanHandler.SendCanFrame(CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_VOL_UP , 0x00 });
        public static void PressVolDown() => CanHandler.SendCanFrame(CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_VOL_DOWN, 0x00 });
        public static void PressVoice() => CanHandler.SendCanFrame(CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_VOICE, 0x01 });
        public static void PressPhone() => CanHandler.SendCanFrame(CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_PHONE, 0x00 });
        public static void PressDiamond() => CanHandler.SendCanFrame(CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_DIAMOND_STAR_HORN, CanValue.STEERING_WHEEL_BUTTON_DIAMOND });
        public static void PressStar() => CanHandler.SendCanFrame(CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_DIAMOND_STAR_HORN, CanValue.STEERING_WHEEL_BUTTON_STAR });
        public static void PressUp() => CanHandler.SendCanFrame(CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_UP, 0x00 });
        public static void PressDown() => CanHandler.SendCanFrame(CanID.STEERING_WHEEL_CONTROLS, new byte[] { CanValue.STEERING_WHEEL_BUTTON_DOWN, 0x00 });
    }

    public class iDriveController
    {
        private static CanHandler CanHandler => BMWControl.CanHandler;

        public static List<Action> OnMenuPressed = new List<Action>();
        public static List<Action> OnPress = new List<Action>();
        public static List<Action> OnUp = new List<Action>();
        public static List<Action> OnDown = new List<Action>();
        public static List<Action> OnLeft = new List<Action>();
        public static List<Action> OnRight = new List<Action>();
        public static List<Action> OnSpinSensitive = new List<Action>();
        public static List<Action> OnSpinRough = new List<Action>();

        public static void PressMenu() => CanHandler.SendCanFrame(CanID.IDRIVE_CONTROLLER, new byte[] { CanValue.IDRIVE_CONTROLLER_MENU_PRESS, CanValue.IDRIVE_CONTROLLER_MENU });
        public static void PressButton() => CanHandler.SendCanFrame(CanID.IDRIVE_CONTROLLER, new byte[] { CanValue.IDRIVE_CONTROLLER_MENU_PRESS, CanValue.IDRIVE_CONTROLLER_PRESS });
        public static void PressUp() => CanHandler.SendCanFrame(CanID.IDRIVE_CONTROLLER, new byte[] { CanValue.IDRIVE_CONTROLLER_UP, 0xC0 });
        public static void PressDown() => CanHandler.SendCanFrame(CanID.IDRIVE_CONTROLLER, new byte[] { CanValue.IDRIVE_CONTROLLER_DOWN, 0xC0 });
        public static void PressLeft() => CanHandler.SendCanFrame(CanID.IDRIVE_CONTROLLER, new byte[] { CanValue.IDRIVE_CONTROLLER_LEFT, 0xC0 });
        public static void PressRight() => CanHandler.SendCanFrame(CanID.IDRIVE_CONTROLLER, new byte[] { CanValue.IDRIVE_CONTROLLER_RIGHT, 0xC0 });
    }
}
