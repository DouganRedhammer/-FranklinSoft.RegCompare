using FranklinSoft.RegCompare.Models;
using Microsoft.Win32;

namespace FranklinSoft.RegCompare
{
    public class RegistryEntryDifference
    {
        public string Name { get; set; }
        public RegistryValueKind Type { get; set; }
        public RegistryKeyData MachineAData { get; set; }
        public RegistryKeyData MachineBData { get; set; }
        public string Location { get; set; }
    }
}
