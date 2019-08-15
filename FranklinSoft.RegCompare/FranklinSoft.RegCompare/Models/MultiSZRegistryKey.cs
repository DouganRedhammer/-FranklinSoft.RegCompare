using System.Text;

namespace FranklinSoft.RegCompare.Models
{
    public class MultiSZRegistryKey: RegistryKeyData
    {
        public string[] Data { get; set; }

        public MultiSZRegistryKey()
        {
            Data = new []{""};
        }

        public MultiSZRegistryKey(object data)
        {
            Data = (string[]) data;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string data in Data)
            {
                sb.Append(data + " ");
            }

            return sb.ToString();

        }

        public override string ToRegistryFileString()
        {
            return "hex(7):" + ConvertUtility.ToHexStringX7(Data);
        }

        public override object GetData()
        {
            return Data;
        }
    }
}
