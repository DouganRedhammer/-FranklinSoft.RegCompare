using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranklinSoft.RegCompare
{
    public static class ConvertUtility
    {
        public static string ByteToHexString(byte[] byteArray)
        {
            string hexString = BitConverter.ToString(byteArray);
            if (hexString != null)
            {
                return hexString.Replace("-", ",");
            }

            return string.Empty;
        }

        public static string ToHexStringX7(string[] hexArray)
        {
            string hexStringX7 = string.Empty;
            if (hexArray.Length > 0)
            {
                for (int i = 0; i < hexArray.Length; i++)
                {
                    if (i != 0)
                    {
                        hexStringX7 += ",";
                    }

                    string hexEntry = hexArray[i];
                    if (hexEntry != null)
                    {
                        hexStringX7 += ToHexStringX2(hexEntry);
                    }
                }

                hexStringX7 += ",00,00";
            }

            return hexStringX7;
        }

        public static string ToHexStringX2(string hexEntry)
        {
            string hexStringX2 = string.Empty;
            if (hexEntry.Length > 0)
            {
                for (int i = 0; i < hexEntry.Length; i++)
                {
                    if (!string.IsNullOrEmpty(hexStringX2))
                    {
                        hexStringX2 += ",";
                    }

                    string item = string.Format(CultureInfo.InvariantCulture, "{0:x2},00", (byte)hexEntry[i]);
                    hexStringX2 += item;
                }

                hexStringX2 += ",00,00";
            }

            return hexStringX2;
        }
        public static byte[] IntToByteArray(Int64 I64)
        {
            return BitConverter.GetBytes(I64);
        }
    }
}
