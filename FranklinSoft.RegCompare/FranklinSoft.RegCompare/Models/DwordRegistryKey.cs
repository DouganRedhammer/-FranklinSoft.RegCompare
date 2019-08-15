namespace FranklinSoft.RegCompare.Models
{
    public class DwordRegistryKey : RegistryKeyData
    {
        public int Data { get; set; }

        public DwordRegistryKey()
        {
            Data = 0;
        }

        public DwordRegistryKey(int data)
        {
            Data = data;
        }

        public DwordRegistryKey(object data)
        {
            Data = (int) data;
        }
        public override string ToString()
        {
            return Data.ToString();
        }

        public override string ToRegistryFileString()
        {
            return "dword:" + Data;
        }

        public override object GetData()
        {
            return Data;
        }
    }
}
