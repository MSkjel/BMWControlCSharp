using BMWControl.CarHandlers;
using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BMWControl.Handlers
{
    public class ServerHandler
    {
        private CarHandler CarHandler => BMWControl.CarHandler;
        private DoorHandler DoorHandler => CarHandler.DoorHandler;
        private EngineHandler EngineHandler => CarHandler.EngineHandler;
        private LightHandler LightHandler => CarHandler.LightHandler;
        private MultiMediaHandler MultiMediaHandler => CarHandler.MultiMediaHandler;
        private SeatHandler SeatHandler => CarHandler.SeatHandler;
        private SpeedHandler SpeedHandler => CarHandler.SpeedHandler;

        private const string ServerIP = "sinxclan.net";
        private const int Port = 747;

        private Stopwatch NetworkWatch = new Stopwatch();

        public ServerHandler()
        {
            Task.Factory.StartNew(() => NetworkLoop());
        }

        public void NetworkLoop()
        {
            try
            {
                NetworkWatch.Start();

                while (BMWControl.ConfigHandler.Run)
                {
                    try
                    {
                        if (NetworkWatch.ElapsedMilliseconds > 1000)
                        {
                            CheckReceivedMessage(GETString(NetworkID.PING));

                            NetworkWatch.Restart();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void CheckReceivedMessage(string message)
        {
            string[] split = message.Split('\\');

            switch(split[0])
            {
                case NetworkID.GET_STATUS:
                    GETString(GetStatusUpdate());
                    break;

                case NetworkID.NEW_UPDATE_AVAILABLE:
                    break;

                case "STOP":
                    BMWControl.ConfigHandler.Run = false;
                    break;
            }
        }

        public string GETString(string message)
        {
            try
            {
                Console.WriteLine("Sending: " + message);

                using (TcpClient webhandle = new TcpClient(ServerIP, Port))
                {
                    webhandle.ReceiveTimeout = 3000;
                    webhandle.SendTimeout = 3000;

                    using (NetworkStream nStream = webhandle.GetStream())
                    {
                        nStream.WriteTimeout = 3000;
                        nStream.ReadTimeout = 3000;

                        string req = message;

                        byte[] data = Encoding.UTF8.GetBytes("CAR\\" + req);
                        nStream.Write(data, 0, data.Length);

                        byte[] receive = new byte[webhandle.ReceiveBufferSize];
                        int count = nStream.Read(receive, 0, (webhandle.ReceiveBufferSize));
                        string response = Encoding.UTF8.GetString(receive, 0, count);

                        if (!response.ToLower().Contains("error"))
                            return response;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());

                return null;
            }
        }

        public string GetStatusUpdate()
        {
            string message = $"{NetworkID.SEND_STATUS}\\" +
                $"{CarHandler.NiceName}\\" +
                $"{DateTime.Now.ToString()}\\" +
                $"{CarHandler.Odometer}\\" +
                $"{CarHandler.Range}\\" +
                $"{CarHandler.TankLevel}\\" +
                $"{CarHandler.BatteryVoltage}\\" +
                $"{DoorHandler.CarLockStatus}\\" +
                $"{DoorHandler.Doors.Driver}\\" +
                $"{DoorHandler.Doors.Passenger}\\" +
                $"{DoorHandler.Doors.DriverRear}\\" +
                $"{DoorHandler.Doors.PassengerRear}\\" +
                $"{DoorHandler.Doors.Trunk}\\" +
                $"{CarHandler.SteeringWheelAngle}\\" +
                $"{EngineHandler.Ignition}\\" +
                $"{EngineHandler.RPM}\\" +
                $"{EngineHandler.Throttle}" +
                $"{EngineHandler.Torque}\\" +
                $"{EngineHandler.AverageFuelUsage}\\" +
                $"{EngineHandler.Temperatures.Coolant}\\" +
                $"{SpeedHandler.Speeds.Average}\\" +
                $"{SpeedHandler.Speeds.Vehicle}\\" +
                $"{SpeedHandler.Speeds.FrontLeft}\\" +
                $"{SpeedHandler.Speeds.FrontRight}\\" +
                $"{SpeedHandler.Speeds.RearLeft}\\" +
                $"{SpeedHandler.Speeds.RearRight}\\";

            return message;
        }

        public void CheckForUpdate()
        {
            
        }

        public void DownloadUpdate()
        {
            using (TcpClient webhandle = new TcpClient(ServerIP, Port))
            {
                webhandle.ReceiveTimeout = 300;
                webhandle.SendTimeout = 300;

                using (NetworkStream nStream = webhandle.GetStream())
                {
                    nStream.WriteTimeout = 300;
                    nStream.ReadTimeout = 300;

                    byte[] data = Encoding.UTF8.GetBytes(NetworkID.GET_UPDATE);
                    nStream.Write(data, 0, data.Length);


                    byte[] receive = new byte[webhandle.ReceiveBufferSize];
                    int bytes;

                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Backup/"))
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Backup/");

                    File.Move(Directory.GetCurrentDirectory() + "BMWControl.exe", Directory.GetCurrentDirectory() + $"/Backup/BMWControl.exe.{DateTime.Now.Minute.ToString()}");

                    using (var output = File.Create(Directory.GetCurrentDirectory() + "/BMWControl.exe"))
                        while ((bytes = nStream.Read(receive, 0, (webhandle.ReceiveBufferSize))) > 0)
                        {
                            output.Write(receive, 0, bytes);
                        }
                }
            }
        }


        public string GetFileMD5(string FileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(FileName))
                {
                    StringBuilder sb = new StringBuilder();
                    byte[] MD5 = md5.ComputeHash(stream);

                    for (int x = 0; x < MD5.Length; x++)
                        sb.Append(MD5[x].ToString("X2"));

                    return sb.ToString();
                }
            }
        }
    }
}
