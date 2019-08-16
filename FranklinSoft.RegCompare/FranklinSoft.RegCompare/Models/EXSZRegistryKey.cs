namespace FranklinSoft.RegCompare.Models
{
    public class EXSZRegistryKey: RegistryKeyData
    {
        protected bool Equals(EXSZRegistryKey other)
        {
            return string.Equals(Data, other.Data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EXSZRegistryKey) obj);
        }

        public override int GetHashCode()
        {
            return (Data != null ? Data.GetHashCode() : 0);
        }

        public static bool operator ==(EXSZRegistryKey left, EXSZRegistryKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EXSZRegistryKey left, EXSZRegistryKey right)
        {
            return !Equals(left, right);
        }

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
