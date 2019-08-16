namespace FranklinSoft.RegCompare.Models
{
    public class DwordRegistryKey : RegistryKeyData
    {
        protected bool Equals(DwordRegistryKey other)
        {
            return Data == other.Data;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DwordRegistryKey) obj);
        }

        public override int GetHashCode()
        {
            return Data;
        }

        public static bool operator ==(DwordRegistryKey left, DwordRegistryKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DwordRegistryKey left, DwordRegistryKey right)
        {
            return !Equals(left, right);
        }

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
