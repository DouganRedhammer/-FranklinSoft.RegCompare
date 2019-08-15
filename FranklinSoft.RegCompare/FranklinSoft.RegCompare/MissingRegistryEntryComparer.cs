using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FranklinSoft.RegCompare;

namespace FranklinSoft.RegCompare
{
    public class MissingRegistryEntryComparer : IEqualityComparer<RegistryEntry>
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
            return a.Location.Equals(b.Location, StringComparison.OrdinalIgnoreCase) && a.Name.Equals(b.Name, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(RegistryEntry obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return obj.FullPath.GetHashCode();
        }
    }
}
