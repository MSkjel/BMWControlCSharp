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

                        case CanValue.STEERING_WHEEL_BUTTON_PHONE_DIAMOND_STAR_HORN:
                            if (canFrame.Data[1] == CanValue.STEERING_WHEEL_BUTTON_PHONE)
                                MultiMediaButtonHandler.SteeringWheelPhone.OnPressed();
                            else if (canFrame.Data[1] == CanValue.STEERING_WHEEL_BUTTON_DIAMOND)
                                MultiMediaButtonHandler.SteeringWheelDiamond.OnPressed();
                            else if (canFrame.Data[1] == CanValue.STEERING_WHEEL_BUTTON_STAR)
                                MultiMediaButtonHandler.SteeringWheelStar.OnPressed();
                            else if (canFrame.Data[1] == CanValue.STEERING_WHEEL_BUTTON_HORN)
                                MultiMediaButtonHandler.SteeringWheelHorn.OnPressed();
                            else
                            {
                                MultiMediaButtonHandler.SteeringWheelPhone.OnReleased();
                                MultiMediaButtonHandler.SteeringWheelDiamond.OnReleased();
                                MultiMediaButtonHandler.SteeringWheelStar.OnReleased();
                                MultiMediaButtonHandler.SteeringWheelHorn.OnReleased();
                            }
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
                    switch (canFrame.Data[0])
                    {
                        case CanValue.IDRIVE_CONTROLLER_MENU_PRESS:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_MENU)
                                MultiMediaButtonHandler.iDriveMenu.OnPressed();
                            else if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_PRESS)
                                MultiMediaButtonHandler.iDrivePress.OnPressed();
                            else
                            {
                                MultiMediaButtonHandler.iDriveMenu.OnReleased();
                                MultiMediaButtonHandler.iDrivePress.OnReleased();
                            }
                            break;

                        case CanValue.IDRIVE_CONTROLLER_UP:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                MultiMediaButtonHandler.iDriveUp.OnPressed();
                            else
                                MultiMediaButtonHandler.iDriveUp.OnReleased();
                            break;

                        case CanValue.IDRIVE_CONTROLLER_DOWN:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                MultiMediaButtonHandler.iDriveDown.OnPressed();
                            else
                                MultiMediaButtonHandler.iDriveDown.OnReleased();
                            break;

                        case CanValue.IDRIVE_CONTROLLER_LEFT:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                MultiMediaButtonHandler.iDriveLeft.OnPressed();
                            else
                                MultiMediaButtonHandler.iDriveLeft.OnReleased();
                            break;

                        case CanValue.IDRIVE_CONTROLLER_RIGHT:
                            if (canFrame.Data[1] == CanValue.IDRIVE_CONTROLLER_UP_DOWN_LEFT_RIGHT)
                                MultiMediaButtonHandler.iDriveRight.OnPressed();
                            else
                                MultiMediaButtonHandler.iDriveRight.OnReleased();
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
