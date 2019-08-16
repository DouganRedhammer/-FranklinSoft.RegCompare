# FranklinSoft.RegCompare
![Nuget](https://img.shields.io/nuget/v/FranklinSoft.RegCompare?style=for-the-badge) ![GitHub tag (latest SemVer)](https://img.shields.io/github/tag/DouganRedhammer/FranklinSoft.RegCompare?style=for-the-badge)

### RegCompare is a Windows Registry helper.
- Find matching differences.
- Find missing keys
- Save missing results to a .reg file
- Async calls to the Windows Registy 
### Installation

Nuget install 
```sh
PM> Install-Package FranklinSoft.RegCompare -Version 1.0.0
```
[Nuget project page](https://www.nuget.org/packages/FranklinSoft.RegCompare/)

### Examples:
##### Test Connection

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

##### Get Registry keys
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

##### Save/Export Missing Entries
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

##### Save/Export Matching Entries
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

## Release Notes
* FindMtchingEntries bug fix.
* Added more unit tests
##### Added async methods with and without CancellationToken:
* TestConnectionAsync
* GetRegistryEntriesAsync



License
----

MIT
