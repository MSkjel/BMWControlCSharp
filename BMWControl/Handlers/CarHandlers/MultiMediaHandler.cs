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
                    switch(canFrame.Data[0])
                    {
                        case CanValue.STEERING_WHEEL_BUTTON_VOL_UP:
                            MultiMediaButtonHandler.SteeringWheelVolUp.OnPressed();
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_VOL_DOWN:
                            MultiMediaButtonHandler.SteeringWheelVolDown.OnPressed();
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_VOICE:
                            MultiMediaButtonHandler.SteeringWheelVoice.OnPressed();
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_PHONE:
                            MultiMediaButtonHandler.SteeringWheelPhone.OnPressed();
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_DIAMOND_STAR_HORN:
                            if (canFrame.Data[1] == CanValue.STEERING_WHEEL_BUTTON_DIAMOND)
                                MultiMediaButtonHandler.SteeringWheelDiamond.OnPressed();
                            else if (canFrame.Data[1] == CanValue.STEERING_WHEEL_BUTTON_STAR)
                                MultiMediaButtonHandler.SteeringWheelStar.OnPressed();
                            else if (canFrame.Data[1] == CanValue.STEERING_WHEEL_BUTTON_HORN)
                                MultiMediaButtonHandler.SteeringWheelHorn.OnPressed();
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_UP:
                            MultiMediaButtonHandler.SteeringWheelUp.OnPressed();
                            break;

                        case CanValue.STEERING_WHEEL_BUTTON_DOWN:
                            MultiMediaButtonHandler.SteeringWheelDown.OnPressed();
                            break;

                        default:
                            foreach (IButtonEvent buttonEvent in MultiMediaButtonHandler.SteeringWheelButtons)
                                buttonEvent.OnReleased();
                            break;
                    }

                    break;

                case CanID.IDRIVE_CONTROLLER:
                    Console.WriteLine(canFrame.ToString());
                    switch (canFrame.Data[0])
                    {
                        case CanValue.IDRIVE_CONTROLLER_MENU_PRESS:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_MENU)
                                MultiMediaButtonHandler.iDriveMenu.OnPressed();
                            else if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_PRESS)
                                MultiMediaButtonHandler.iDrivePress.OnPressed();
                            break;

                        case CanValue.IDRIVE_CONTROLLER_UP:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                MultiMediaButtonHandler.iDriveUp.OnPressed();
                            break;

                        case CanValue.IDRIVE_CONTROLLER_DOWN:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                MultiMediaButtonHandler.iDriveDown.OnPressed();
                            break;

                        case CanValue.IDRIVE_CONTROLLER_LEFT:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                MultiMediaButtonHandler.iDriveLeft.OnPressed();
                            break;

                        case CanValue.IDRIVE_CONTROLLER_RIGHT:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                MultiMediaButtonHandler.iDriveRight.OnPressed();
                            break;

                        default:
                            foreach (IButtonEvent buttonEvent in MultiMediaButtonHandler.iDriveButtons)
                                buttonEvent.OnReleased();
                            break;
                    }

                break;
            }
        }
    }
}
