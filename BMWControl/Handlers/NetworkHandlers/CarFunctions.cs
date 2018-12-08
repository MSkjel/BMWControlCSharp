using BMWControl.CarHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.Handlers.NetworkHandlers
{
    public class CarFunctions
    {
        private CarHandler CarHandler => BMWControl.CarHandler;

        public static void CheckCommand(IEnumerable<string> command)
        {
            switch(command.ElementAtOrDefault(0).ToLowerInvariant())
            {
                case "lock":
                    CarHandler.DoorHandler.LockCar();
                    break;

                case "unlock":
                    CarHandler.DoorHandler.UnlockCar();
                    break;
            }
        }
    }
}
