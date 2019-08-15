using System.Collections.Generic;

namespace FranklinSoft.RegCompare
{
    public class RegistryEntryComparer : IEqualityComparer<RegistryEntry>
    {
        public bool Equals(RegistryEntry a, RegistryEntry b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (object.ReferenceEquals(a, null) ||
                object.ReferenceEquals(b, null))
            {
                return false;
            }
            
            bool x = (a.Data.Equals(b.Data) && a.Location.Equals(b.Location) && a.Name.Equals(b.Name) && a.Type.Equals(b.Type));
            return (a.Data.Equals(b.Data) && a.Location.Equals(b.Location) && a.Name.Equals(b.Name) && a.Type.Equals(b.Type));
        }

        public int GetHashCode(RegistryEntry obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return obj.Data.GetHashCode() + obj.Location.GetHashCode() + obj.Name.GetHashCode() + obj.Type.GetHashCode();
        }
    }
}
