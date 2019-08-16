using System;

namespace FranklinSoft.RegCompare.Models
{
    public class SZRegistryKey : RegistryKeyData
    {
        protected bool Equals(SZRegistryKey other)
        {
            return string.Equals(Data, other.Data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SZRegistryKey) obj);
        }

        public override int GetHashCode()
        {
            return (Data != null ? Data.GetHashCode() : 0);
        }

        public static bool operator ==(SZRegistryKey left, SZRegistryKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SZRegistryKey left, SZRegistryKey right)
        {
            return !Equals(left, right);
        }

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
