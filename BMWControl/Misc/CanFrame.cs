using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.Misc
{
    public class CanFrame
    {
        public int CanID;
        public int Length;

        public byte[] Data;

        public bool Extended = false;
        public bool RTR = false;

        public CanFrame(int CanID, byte[] Data)
        {
            this.CanID = CanID;
            this.Length = Data.Length;
            this.Data = Data;
        }

        public CanFrame()
        {

        }
    }
}
