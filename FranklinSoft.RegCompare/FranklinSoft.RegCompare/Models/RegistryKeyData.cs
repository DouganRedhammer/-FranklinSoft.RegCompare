namespace FranklinSoft.RegCompare.Models
{
    public abstract class RegistryKeyData
    {
        public abstract override string ToString();
        public abstract string ToRegistryFileString();

        public abstract object GetData();
    }
}
