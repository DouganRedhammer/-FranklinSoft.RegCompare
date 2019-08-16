using System;

namespace FranklinSoft.RegCompare.Models
{
    public class QwordRegistryKey: RegistryKeyData
    {
        public string Data { get; set; }

        protected bool Equals(QwordRegistryKey other)
        {
            return string.Equals(Data, other.Data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((QwordRegistryKey) obj);
        }

        public override int GetHashCode()
        {
            return (Data != null ? Data.GetHashCode() : 0);
        }

        public static bool operator ==(QwordRegistryKey left, QwordRegistryKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(QwordRegistryKey left, QwordRegistryKey right)
        {
            return !Equals(left, right);
        }

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
