using System.Text;

namespace FranklinSoft.RegCompare.Models
{
    public class MultiSZRegistryKey: RegistryKeyData
    {
        protected bool Equals(MultiSZRegistryKey other)
        {
            return Equals(Data, other.Data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MultiSZRegistryKey) obj);
        }

        public override int GetHashCode()
        {
            return (Data != null ? Data.GetHashCode() : 0);
        }

        public static bool operator ==(MultiSZRegistryKey left, MultiSZRegistryKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MultiSZRegistryKey left, MultiSZRegistryKey right)
        {
            return !Equals(left, right);
        }

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
