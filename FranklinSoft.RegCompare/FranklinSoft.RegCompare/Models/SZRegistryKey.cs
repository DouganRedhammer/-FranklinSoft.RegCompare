using System;

namespace FranklinSoft.RegCompare.Models
{
    public class SZRegistryKey : RegistryKeyData
    {
        public string Data { get; set; }
        public SZRegistryKey()
        {
            Data = String.Empty;
        }

        public SZRegistryKey(string data)
        {
            Data = data;
        }

        public SZRegistryKey(object data)
        {
            Data = data.ToString();
        }

        public override string ToString()
        {
            return Data;
        }

        public override string ToRegistryFileString()
        {
            return "\"" + Data + "\"";
        }

        public override object GetData()
        {
            return Data;
        }
    }
}
