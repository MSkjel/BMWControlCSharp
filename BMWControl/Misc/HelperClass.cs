using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWControl.Misc
{
    public class HelperClass
    {
        public static int GetHexReversedValueInt(IEnumerable<byte> bytes)
        {
            string[] arr = bytes.Select(x => x.ToString("X")).ToArray();
            string final = "";

            Array.Reverse(arr);

            foreach (string s in arr)
            {
                final += s;
            }

            return int.Parse(final, NumberStyles.HexNumber);
        }

        public static float GetHexReversedValueFloat(IEnumerable<byte> bytes)
        {
            string[] arr = bytes.Select(x => x.ToString("X")).ToArray();
            string final = "";

            Array.Reverse(arr);

            foreach (string s in arr)
            {
                final += s;
            }

            return Convert.ToSingle(int.Parse(final, NumberStyles.HexNumber));
        }

        public static IEnumerable<byte> GetReversedBytes(int dec)
        {
            byte[] buffer = BitConverter.GetBytes(dec).Take(2).ToArray();

            Array.Reverse(buffer);

            return buffer;
        }

        public static string DecodeVIN(byte[] bytes)
        {
            string VIN = "";

            for (int i = 0; i <= 6; i++)
                VIN += (char)bytes[i];

            return VIN;
        }

        public static byte GetMSB(int shortValue)
        {
            return (byte)((shortValue & 0xF0) >> 4);
        }

        public static int GetMSBNoShift(int shortValue)
        {
            return (shortValue & 0xF0);
        }

        public static byte GetLSB(int shortValue)
        {
            return (byte)(shortValue & 0x0F);
        }

        public static int GetMSBAsHex(int shortValue)
        {
            return int.Parse(GetMSB(shortValue).ToString("X2")[1].ToString(), NumberStyles.HexNumber);
        }

        public static int GetLSBAsHex(int shortValue)
        {
            return int.Parse(GetLSB(shortValue).ToString("X2")[1].ToString(), NumberStyles.HexNumber);
        }

        public static int GetSingleBitAsInt(byte b, int index)
        {
            return int.Parse(b.ToString("X2")[index].ToString());
        }

        public static string ByteArrayToStringX2(byte[] array)
        {
            return string.Join("/", array.Select(x => x.ToString("X2")));
        }
    }
}
