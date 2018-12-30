using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace BMWControlApp
{
    [Activity(Label = "BMW Control", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class MainActivity : Activity
    {
        private RecyclerView rv;
        private MainAdapter adapter;
        private static string VIN = "Test1";

        /*
         * Oncreate method
         */
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //RV
            rv = FindViewById<RecyclerView>(Resource.Id.mRecylcerID);
            rv.SetLayoutManager(new LinearLayoutManager(this));
            rv.SetItemAnimator(new DefaultItemAnimator());

            Button button = FindViewById<Button>(Resource.Id.okButton);
            EditText text = FindViewById<EditText>(Resource.Id.inputText);

            button.Click += delegate 
            {
                VIN = text.Text;

                adapter = new MainAdapter(GetCarInfo());
                rv.SetAdapter(adapter);
            };
        }

        public static string GETString(string message)
        {
            try
            {
                using (TcpClient webhandle = new TcpClient("sinxclan.net", 747))
                {
                    webhandle.ReceiveTimeout = 10000;
                    webhandle.SendTimeout = 10000;

                    using (NetworkStream nStream = webhandle.GetStream())
                    {
                        nStream.WriteTimeout = 10000;
                        nStream.ReadTimeout = 10000;

                        string req = message;

                        byte[] data = Encoding.UTF8.GetBytes(req);
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
                Console.WriteLine(e.ToString());

                return null;
            }
        }

    public static Dictionary<string, string> GetCarInfo()
        {
            Car car = JsonConvert.DeserializeObject<Car>(GETString($"CLIENT\\GET_STATUS\\{VIN}"));

            try
            {
                    return new Dictionary<string, string>()
                    {
                        { "CarStatus",
                            $"Last Update: {((DateTime.Now - car.LastUpdate).TotalSeconds > 0 ? (int)(DateTime.Now - car.LastUpdate).TotalSeconds : 0)} second(s)\n" +
                            $"Last CanEvent: {((DateTime.Now - car.LastCanEvent).TotalSeconds > 0 ? (int)(DateTime.Now - car.LastCanEvent).TotalSeconds : 0)} second(s)\n" +
                            $"Name: {car.NiceName}\n" +
                            $"VIN: {car.VIN}\n" +
                            $"Odometer: {car.Odometer}\n" +
                            $"Range: {car.Range}\n" +
                            $"BatteryVoltage: {car.BatteryVoltage}"
                        },
                        { "EngineHandler",
                            $"RPM: {car.RPM}\n" +
                            $"OilPressure: {car.OilPressure}\n" +
                            $"Throttle: {car.Throttle}\n" +
                            $"Torque: {car.Torque}\n" +
                            $"AverageFuelUsage: {car.AverageFuelUsage}\n" +
                            $"Coolant Temperature: {car.Temperatures.Coolant}\n" +
                            $"Ignition: {car.Ignition}"
                        },
                        { "DoorHandler",
                            $"CarLockStatus: {car.CarLockStatus}\n" +
                            $"Driver: {car.Doors.Driver}\n" +
                            $"Passenger: {car.Doors.Passenger}\n" +
                            $"Driver Rear: {car.Doors.DriverRear}\n" +
                            $"Passenger Rear: {car.Doors.PassengerRear}\n" +
                            $"Trunk: {car.Doors.Trunk}\n"
                        },
                        { "SpeedHandler",
                            $"Vehicle: {car.Speeds.Vehicle}\n" +
                            $"Average: {car.Speeds.Average}\n" +
                            $"Front Left: {car.Speeds.FrontLeft}\n" +
                            $"Front Right: {car.Speeds.FrontRight}\n" +
                            $"Rear Left: {car.Speeds.RearLeft}\n" +
                            $"Rear Right: {car.Speeds.RearRight}"
                        },
                        { "ClimateHandler",
                            $"Outside: {car.Temperatures.OutsideTemperature}\n" +
                            $"Inside Center: {car.Temperatures.InteriorTemperatureClimate}\n" +
                            $"Driver Actual: {car.Temperatures.InteriorTemperatureDriverActual}\n" +
                            $"Passenger Actual: {car.Temperatures.InteriorTemperaturePassengerActual}\n" +
                            $"Driver Setpoint: {car.Temperatures.DriverTemperatureSetpoint}\n" +
                            $"Passenger Setpoint: {car.Temperatures.PassengerTemperatureSetpoint}"
                        }
                    };
            }
            catch(Exception e)
            {
                return new Dictionary<string, string>();
            }
        }
    }
}