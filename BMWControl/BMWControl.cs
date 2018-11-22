using BMWControl.CarHandlers;
using BMWControl.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BMWControl
{
    public class BMWControl
    {
        public static CanEventHandler CanEventHandler;
        public static CanHandler CanHandler;
        public static CarHandler CarHandler;
        public static ServerHandler ServerHandler;
        public static ConfigHandler ConfigHandler;

        static void Main(string[] args)
        {
            InitializeSingletons();
        }

        private static void InitializeSingletons()
        {
            ConfigHandler = new ConfigHandler();
            ServerHandler = new ServerHandler();

            CanHandler = new CanHandler();
            CanEventHandler = new CanEventHandler();
            CarHandler = new CarHandler();

            Console.ReadLine();

            ConfigHandler.Run = false;
        }
    }
}
