using BMWControl.Handlers;
using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.CanEvents
{
    public class IButtonEvent
    {
        public CanHandler CanHandler => BMWControl.CanHandler;
        public CanEventHandler CanEventHandler => BMWControl.CanEventHandler;

        private List<Action> OnPressListeners = new List<Action>();
        private List<Action> OnReleaseListeners = new List<Action>();

        public string Name = "";

        public bool IsPressed = false;

        public CanFrame CanFrame;


        public IButtonEvent(string Name)
        {
            this.Name = Name;
        }

        public IButtonEvent(string Name, int CanID, byte[] Data)
        {
            this.Name = Name;
            CanFrame = new CanFrame(CanID, Data);
        }

        public IButtonEvent(string Name, CanFrame canFrame)
        {
            this.Name = Name;
            CanFrame = canFrame;
        }

        public void PressButton()
        {
            if(CanFrame != null)
                CanHandler.SendCanFrame(CanFrame);
            else
                Console.WriteLine($"Not sending PressButton from {Name}. No CanFrame registered");
        }

        public void OnPressed()
        {
            Console.WriteLine($"{Name} has been pressed");
            IsPressed = true;

            foreach(Action act in OnPressListeners)
            {
                try
                {
                    act.Invoke();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void OnReleased()
        {
            if (IsPressed)
            {
                Console.WriteLine($"{Name} has been released");

                foreach (Action act in OnReleaseListeners)
                {
                    try
                    {
                        act.Invoke();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        public void AddPressListener(Action action)
        {
            OnPressListeners.Add(action);
        }
        
        public void AddReleaseListener(Action act)
        {
            OnReleaseListeners.Add(act);
        }

        public void RemovePressListener(Action action)
        {

        }

        public void RemoveReleaseListener(Action action)
        {

        }
    }
}
