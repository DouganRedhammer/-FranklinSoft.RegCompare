namespace FranklinSoft.RegCompare.Models
{
    public class EXSZRegistryKey: RegistryKeyData
    {
        public string Data { get; set; }

        public EXSZRegistryKey()
        {
            Data = string.Empty;
        }

        public EXSZRegistryKey(object data)
        {
            Data = data.ToString();
        }

        public override string ToString()
        {
            return Data;
        }

        public override string ToRegistryFileString()
        {
            return "hex(2):" + ConvertUtility.ToHexStringX2(Data);
        }

        public override object GetData()
        {
            return Data;
        }
    }
}
