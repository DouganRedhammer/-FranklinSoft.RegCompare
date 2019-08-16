namespace FranklinSoft.RegCompare.Models
{
    public class BinaryRegistryKey: RegistryKeyData
    {
        protected bool Equals(BinaryRegistryKey other)
        {
            return string.Equals(Data, other.Data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BinaryRegistryKey) obj);
        }

        public override int GetHashCode()
        {
            return (Data != null ? Data.GetHashCode() : 0);
        }

        public static bool operator ==(BinaryRegistryKey left, BinaryRegistryKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BinaryRegistryKey left, BinaryRegistryKey right)
        {
            return !Equals(left, right);
        }

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
