using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.Handlers
{
    public class CanHandler
    {
        //Singletons
        private ConfigHandler ConfigHandler => BMWControl.ConfigHandler;
        private CanEventHandler CanEventHandler => BMWControl.CanEventHandler;

        private const string CanDevice = "can0";

        [DllImport("libCanWrapper.so",
              CallingConvention = CallingConvention.Cdecl)]
        private static extern bool Init(string device, ref int errorCode);

        [DllImport("libCanWrapper.so",
              CallingConvention = CallingConvention.Cdecl)]
        private static extern bool SendMsg(Can_Frame canframe, bool extended, bool rtr, ref int errorCode);

        [DllImport("libCanWrapper.so",
              CallingConvention = CallingConvention.Cdecl)]
        private static extern bool GetMsg(ref Can_Frame can_frame, ref bool extended, ref bool rtr, ref bool error, ref int errorCode, Timeval timeout);

        [DllImport("libCanWrapper.so",
              CallingConvention = CallingConvention.Cdecl)]
        private static extern bool SetRecvBufferSize(int size);

        [DllImport("libCanWrapper.so",
              CallingConvention = CallingConvention.Cdecl)]
        private static extern void EnableErrorMessages();

        [DllImport("libCanWrapper.so",
              CallingConvention = CallingConvention.Cdecl)]
        private static extern void Close();

        private struct Can_Frame
        {
            public uint can_id;
            public byte can_dlc;
            public byte flags;
            public byte res0;
            public byte res1;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] data;
        }

        private struct Timeval
        {
            public int tv_sec;
            public int tv_usec;
        }

        public CanHandler()
        {
            Task.Factory.StartNew(() =>
            {
                if (Initialize())
                    CanReceiveLoop();
            });
        }

        private bool Initialize()
        {
            int errorCode = 0;

            if (Init(CanDevice, ref errorCode))
            {
                Console.WriteLine($"Successfully initialized {CanDevice}");

                return true;
            }
            else
                Console.WriteLine($"Failed to initialize {CanDevice}");

            return false;
        }

        private void CanReceiveLoop()
        {
            while(ConfigHandler.Run)
            {
                CanEventHandler.CanFrameReceived(ReceiveCanFrame());
            }
        }

        public bool SendCanFrame(int CanID, byte[] Data) => SendCanFrame(new CanFrame(CanID, Data));

        public bool SendCanFrame(CanFrame frame)
        {
            int errorCode = 0;

            Can_Frame cFrame = new Can_Frame()
            {
                can_id = (uint)frame.CanID,
                can_dlc = (byte)frame.Length,
                data = frame.Data
            };

            if (SendMsg(cFrame, frame.Extended, frame.RTR, ref errorCode))
            {
                Console.WriteLine($"Successfully sent frame with CANID: {frame.CanID.ToString("X")}");

                return true;
            }

            Console.WriteLine($"Failed to send frame with CANID: {frame.CanID.ToString("X")}. ErrorCode: {errorCode}");

            return false;
        }

        private CanFrame ReceiveCanFrame(int timeout = int.MaxValue)
        {
            int errorCode = 0;
            bool extended = false;
            bool rtr = false;
            bool error = false;
            Can_Frame frame = new Can_Frame();

            Timeval time = new Timeval
            {
                tv_sec = timeout,
                tv_usec = 0
            };

            if (GetMsg(ref frame, ref extended, ref rtr, ref error, ref errorCode, time))
                return StructToCanFrame(frame);

            return new CanFrame();
        }

        private CanFrame StructToCanFrame(Can_Frame cFrame)
        {
            CanFrame frame = new CanFrame
            {
                CanID = (int)cFrame.can_id,
                Length = cFrame.can_dlc,
                Data = cFrame.data
            };

            return frame;
        }
    }
}
