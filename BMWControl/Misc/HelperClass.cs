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
        //public static int GetHexReversedValueInt(byte[] bytes, int startIndex, int count)
        //{
        //    bytes = bytes.Skip(startIndex).Take(count).ToArray();

        //    if (count <= 2)
        //    {
        //        bytes.Reverse();
        //        return Convert.ToInt32(BitConverter.ToUInt16(bytes, 0));
        //    }
        //    else
        //    {
        //        byte[] buffer = new byte[4];

        //        Array.Copy(bytes, buffer, bytes.Length);

        //        return BitConverter.ToInt32(buffer, 0);
        //    }
        //}

        //public static float GetHexReversedValueFloat(byte[] bytes, int startIndex, int count)
        //{
        //    bytes = bytes.Skip(startIndex).Take(count).ToArray();

        //    if (count <= 2)
        //    {
        //        Console.WriteLine(BitConverter.ToUInt16(bytes, 0));
        //        return Convert.ToSingle(BitConverter.ToUInt16(bytes, 0));
        //    }
        //    else
        //    {
        //        byte[] buffer = new byte[4];

        //        Array.Copy(bytes, buffer, bytes.Length);

        //        return Convert.ToSingle(BitConverter.ToUInt32(buffer, 0));
        //    }
        //}

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

        public static int GetSingleBitAsInt(byte b, int index)
        {
            return int.Parse(b.ToString("X2")[index].ToString());
        }

        //public static byte[] CombineBMWBytes(byte[] bytes)
        //{
        //    string final = "";

        //    foreach(byte b in bytes)
        //    {
        //        string s = b.ToString("X2");

        //        if (s[0] == '0')
        //            final += s[1];
        //        else
        //            final += s;
        //    }

        //    return new byte[] {}
        //}


        public static string ByteArrayToStringX2(byte[] array)
        {
            return string.Join("/", array.Select(x => x.ToString("X2")));
        }
    }
}
