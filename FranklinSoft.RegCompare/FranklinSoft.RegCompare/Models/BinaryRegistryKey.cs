namespace FranklinSoft.RegCompare.Models
{
    public class BinaryRegistryKey: RegistryKeyData
    {
        public string Data { get; set; }

        public BinaryRegistryKey()
        {
            Data = string.Empty;
        }

        public BinaryRegistryKey(object data)
        {
            Data = data.ToString();
        }

        public override string ToString()
        {
            return Data;
        }

        public override string ToRegistryFileString()
        {
            return "hex:" + Data;
        }

        public override object GetData()
        {
            return Data;
        }
    }
}
