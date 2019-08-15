using FranklinSoft.RegCompare.Models;
using Microsoft.Win32;

namespace FranklinSoft.RegCompare
{
    public class RegistryEntry
    {
        public string Name { get; set; }
        public RegistryValueKind Type { get; set; }
        public RegistryKeyData Data { get; set; }
        public string Location { get; set; }

        public string FullPath { get; set; }
    }
}
