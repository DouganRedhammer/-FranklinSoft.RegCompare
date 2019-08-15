# FranklinSoft.RegCompare

RegCompare is a Windows Registry helper.

### Installation

Nuget install
```sh
PM> Install-Package FranklinSoft.RegCompare -Version 1.0.0
```
Examples:

Test Connection

```csharp
TestConnectionResult result = RegistryCompare.TestConnection(machineName);
if (result.Successful)
{
    //do something
}
else
{
    throw  new  Exception(result.Message + " " + result.Exception);
}
```

Get Registry keys
```csharp
string machineName = "The computer name you want to connect to"
string rootKey = "Console"
RegistryHive hive = RegistryHive.CurrentUser

RegistryEntriesResult result = RegistryCompare.GetRegistryEntries(hive, rootKey, machineName);
if (result.Successful)
{
    //do something
}
else
{
    throw  new Exception(result.Message + result.Exception);
}
```

Save/Export Missing Entries
```
List<RegistryEntry> _missingEntriesFromMachineB;
SaveFileDialog saveFileDialog1 = new SaveFileDialog();
saveFileDialog1.Filter = "Registry File|*.reg";
saveFileDialog1.Title = "Save a Registry File";
saveFileDialog1.ShowDialog();

if (saveFileDialog1.FileName != "")
{
    StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile());
    RegFileHandler.ExportMissingEntries(_missingEntriesFromMachineB, sw);
}
```

Save/Export Matching Entries
```
List<RegistryEntryDifference> registryEntryDifferences;
SaveFileDialog saveFileDialog1 = new SaveFileDialog();
saveFileDialog1.Filter = "Text File|*.txt";
saveFileDialog1.Title = "Save File";
saveFileDialog1.ShowDialog();

if (saveFileDialog1.FileName != "")
{
    StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile());
    RegFileHandler.ExportMatchingDifferences(registryEntryDifferences, sw);
}
```


License
----

MIT
