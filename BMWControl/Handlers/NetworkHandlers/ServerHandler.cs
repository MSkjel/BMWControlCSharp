using BMWControl.CarHandlers;
using BMWControl.Handlers.CarHandlers;
using BMWControl.Handlers.NetworkHandlers;
using BMWControl.Misc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private TimeHandler TimeHandler => CarHandler.TimeHandler;
        private ClimateHandler ClimateHandler => CarHandler.ClimateHandler;

        private const string ServerIP = "51.174.232.202";
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
                            CheckReceivedMessage(GETString($"{NetworkID.PING}\\{CarHandler.VIN}"));

                            NetworkWatch.Restart();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    Thread.Sleep(100);
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

                case NetworkID.EXECUTE_COMMAND:
                    CarFunctions.CheckCommand(split.Skip(1));
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
                //Console.WriteLine("Sending: " + message);

                using (TcpClient webhandle = new TcpClient(ServerIP, Port))
                {
                    webhandle.ReceiveTimeout = 1000;
                    webhandle.SendTimeout = 1000;

                    using (NetworkStream nStream = webhandle.GetStream())
                    {
                        nStream.WriteTimeout = 500;
                        nStream.ReadTimeout = 500;

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
            CarHandler.CarTime = DateTime.Now;

            string message =
                $"{NetworkID.SEND_STATUS}\\" +
                $"{CarHandler.VIN}\\";

            JsonMergeSettings settings = new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Concat };
            JObject Car = JObject.FromObject(CarHandler);
            JObject Doors = JObject.FromObject(DoorHandler);
            JObject Engine = JObject.FromObject(EngineHandler);
            JObject Speed = JObject.FromObject(SpeedHandler);
            JObject Time = JObject.FromObject(TimeHandler);
            JObject Climate = JObject.FromObject(ClimateHandler);

            Car.Merge(Engine, settings);
            Car.Merge(Doors, settings);
            Car.Merge(Engine, settings);
            Car.Merge(Speed, settings);
            Car.Merge(Climate, settings);
            Car.Merge(Time, settings);

            message += Car.ToString();

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
