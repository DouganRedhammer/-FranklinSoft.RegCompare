using System;

namespace FranklinSoft.RegCompare.Models
{
    public class QwordRegistryKey: RegistryKeyData
    {
        public string Data { get; set; }
        public QwordRegistryKey()
        {
            this.Data = string.Empty;
        }

        public QwordRegistryKey(object data)
        {
            Data = data.ToString();
        }

        public override string ToString()
        {
            return Data;
        }

        public override string ToRegistryFileString()
        {
            byte[] byteval = ConvertUtility.IntToByteArray(Convert.ToInt64(Data));
            return "hex(b):" + BitConverter.ToString(byteval).Replace("-", ",");
        }

        public override object GetData()
        {
            return Data;
        }
    }
}
