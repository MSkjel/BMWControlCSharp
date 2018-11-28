using BMWControl.CanEvents;
using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BMWControl.Handlers.CarHandlers
{
    public class TimeHandler : ICanEvent
    {
        public DateTime CarTime;

        public TimeHandler()
        {
            CanEventHandler.AddCanEventHandler(this);

            if (ConfigHandler.SetTimeAtBoot)
                Task.Factory.StartNew(() =>
                {
                    DateTime time = DateTime.MinValue;

                    Thread.Sleep(5000);


                    if (!DateTime.TryParse(ServerHandler.GETString(NetworkID.GET_TIME), out time))
                    {
                        time = DateTime.Now;

                        Console.WriteLine("Could not get network time to set car time. Falling back to device time");
                    }

                    SetDateAndTime(time);
                });
        }

        public override void OnCanFrameReceived(CanFrame canFrame)
        {
            switch(canFrame.CanID)
            {
                case CanID.GET_DATE_TIME:
                    CarTime = new DateTime(HelperClass.GetHexReversedValueInt(new byte[] { canFrame.Data[5], canFrame.Data[6] }), 
                        HelperClass.GetMSBAsHex(canFrame.Data[4]), canFrame.Data[3], canFrame.Data[0], canFrame.Data[1], canFrame.Data[2]);
                    break;

                case 0xAF4:
                    Console.WriteLine(canFrame.ToStringInt());
                    break;
            }
        }

        public void SetDateAndTime(DateTime dateTime)
        {
            byte[] year = HelperClass.GetReversedBytes(dateTime.Year).ToArray();
            byte[] date = new byte[8] { (byte)dateTime.Hour, (byte)dateTime.Minute, (byte)dateTime.Second, (byte)dateTime.Day, (byte)int.Parse($"{dateTime.Month.ToString("X")}F", System.Globalization.NumberStyles.HexNumber), year[0], year[1], 0xF2 };

            CanHandler.SendCanFrame(CanID.SET_DATE_TIME, date);
        }
    }
}
