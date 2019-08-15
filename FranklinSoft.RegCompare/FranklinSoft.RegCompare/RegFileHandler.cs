using System.Collections.Generic;
using System.IO;

namespace FranklinSoft.RegCompare
{
    public static class RegFileHandler
    {
        public static void ExportMatchingDifferences(List<RegistryEntryDifference> registryEntryDifferences, StreamWriter stream)
        {
            Dictionary<string, List<RegistryEntryDifference>> registryEntryDifferencesDict = new Dictionary<string, List<RegistryEntryDifference>>();
            foreach (var registryEntryDifference in registryEntryDifferences)
            {
                if (registryEntryDifferencesDict.ContainsKey(registryEntryDifference.Location))
                {
                    var currentItems = registryEntryDifferencesDict[registryEntryDifference.Location];
                    currentItems.Add(registryEntryDifference);
                    registryEntryDifferencesDict[registryEntryDifference.Location] = currentItems;
                }
                else
                {
                    List<RegistryEntryDifference> differences = new List<RegistryEntryDifference>();
                    differences.Add(registryEntryDifference);
                    registryEntryDifferencesDict.Add(registryEntryDifference.Location, differences);
                }
            }

            using (StreamWriter sw = stream)
            {
                foreach (KeyValuePair<string, List<RegistryEntryDifference>> item in registryEntryDifferencesDict)
                {
                    sw.WriteLine(item.Key);
                    foreach (var entryDifference in item.Value)
                    {
                        sw.WriteLine("Name: " + entryDifference.Name);
                        sw.WriteLine("MachineA: " + entryDifference.MachineAData);
                        sw.WriteLine("MachineB: " + entryDifference.MachineBData);
                        sw.WriteLine("Type: " + entryDifference.Type);
                        sw.WriteLine("");
                    }
                }
            }
        }

        public static void ExportMissingEntries(List<RegistryEntry> registryEntries, StreamWriter stream)
        {
            Dictionary<string, List<RegistryEntry>> registryEntryDict = new Dictionary<string, List<RegistryEntry>>();
            foreach (var registryEntryDifference in registryEntries)
            {
                if (registryEntryDict.ContainsKey(registryEntryDifference.Location))
                {
                    var currentItems = registryEntryDict[registryEntryDifference.Location];
                    currentItems.Add(registryEntryDifference);
                    registryEntryDict[registryEntryDifference.Location] = currentItems;
                }
                else
                {
                    List<RegistryEntry> differences = new List<RegistryEntry>();
                    differences.Add(registryEntryDifference);
                    registryEntryDict.Add(registryEntryDifference.Location, differences);
                }
            }

            using (StreamWriter sw = stream)
            {
                sw.WriteLine("Windows Registry Editor Version 5.00");
                sw.WriteLine("");
                foreach (KeyValuePair<string, List<RegistryEntry>> item in registryEntryDict)
                {
                    sw.WriteLine("[" + item.Key + "]");
                    foreach (var entryDifference in item.Value)
                    {
                        sw.Write("\"" + entryDifference.Name + "\"=");
                        sw.Write(entryDifference.Data.ToRegistryFileString());
                        sw.WriteLine("");
                    }
                    sw.WriteLine("");
                }
            }
        }

    }
}
