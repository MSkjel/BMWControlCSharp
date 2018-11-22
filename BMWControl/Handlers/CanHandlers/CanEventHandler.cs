using BMWControl.CanEvents;
using BMWControl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.Handlers
{
    public class CanEventHandler
    {
        public List<Tuple<int, ICanEvent>> CanEvents = new List<Tuple<int, ICanEvent>>();


        public void CanFrameReceived(CanFrame canFrame)
        {
            foreach(Tuple<int, ICanEvent> canEvent in CanEvents.OrderBy(x => x.Item1))
            {
                try
                {
                    canEvent.Item2.OnCanFrameReceived(canFrame);
                    //Console.WriteLine($"Sent CanEvent({canFrame.CanID.ToString("X2")}) to: {canEvent.Item2.Name}");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void AddCanEventHandler(object canEvent, int priority = 0)
        {
            CanEvents.Add(Tuple.Create(priority, canEvent as ICanEvent));

            Console.WriteLine($"Registered new CanEventHandler: {(canEvent as ICanEvent).Name}");
        }
    }
}
