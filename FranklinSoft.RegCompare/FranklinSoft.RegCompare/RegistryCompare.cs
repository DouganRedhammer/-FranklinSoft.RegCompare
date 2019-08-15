using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using FranklinSoft.RegCompare.Models;
using FranklinSoft.RegCompare.Results;
using Microsoft.Win32;
using NLog;
using RegistryKey = Microsoft.Win32.RegistryKey;

namespace FranklinSoft.RegCompare
{
    public static class RegistryCompare
    {
        public static RegistryEntriesResult GetRegistryEntries(RegistryHive registryHive, string rootKey, string machineName)
        {
            RegistryKey hive = null;
            try
            {
                hive = RegistryKey.OpenRemoteBaseKey(registryHive, machineName, RegistryView.Registry64);
                var entries = GetRegistryEntries(rootKey, hive);
                return new RegistryEntriesResult()
                {
                    Successful = true,
                    RegistryEntries = entries ?? new List<RegistryEntry>()
                };
            }
            catch (Exception ex)
            {
                return new RegistryEntriesResult()
                {
                    Successful = false,
                    Exception = ex,
                    Message = ex.Message,
                    RegistryEntries = new List<RegistryEntry>()
                };
            }
            finally
            {
                hive?.Close();
            }
        }

        private static List<RegistryEntry> GetRegistryEntries(string rootKey, RegistryKey hive)
        {
            RegistryKey key;
            List<RegistryEntry> entries = new List<RegistryEntry>();
            key = hive.OpenSubKey(rootKey);
            if (key != null && key.SubKeyCount > 1)
            {
                foreach (var k in key.GetSubKeyNames())
                {
                    string rootKeyLocation = Regex.Replace(key.Name, hive.Name + @"\\", "");
                    RegistryKey subkey = hive.OpenSubKey(rootKeyLocation + "\\" + k);
                    entries.AddRange(GetRegistryEntries(rootKeyLocation + "\\" + k, hive));
                }
            }

            foreach (var valueName in key.GetValueNames())
            {
                var rawValue = key.GetValue(valueName, "", RegistryValueOptions.DoNotExpandEnvironmentNames);
                RegistryKeyData keyValue = null;
                RegistryValueKind kvk = key.GetValueKind(valueName);

                switch (kvk)
                {
                    case RegistryValueKind.String:
                        keyValue = new SZRegistryKey(rawValue);
                        break;

                    case RegistryValueKind.DWord:
                        keyValue = new DwordRegistryKey(rawValue);
                        break;
                    case RegistryValueKind.ExpandString:
                        keyValue = new EXSZRegistryKey(rawValue);
                        break;
                    case RegistryValueKind.MultiString:
                        keyValue = new MultiSZRegistryKey(rawValue);
                        break;
                    case RegistryValueKind.QWord:
                        object keyValue3 = key.GetValue(valueName);
                        keyValue = new QwordRegistryKey(rawValue);
                        break;
                    case RegistryValueKind.Binary:
                        byte[] binary = (byte[])rawValue;
                        var hexstring = ConvertUtility.ByteToHexString(binary);
                        keyValue = new BinaryRegistryKey(hexstring);
                        break;
                    case RegistryValueKind.Unknown:
                    case RegistryValueKind.None:
                        keyValue = new SZRegistryKey(string.Empty);
                        break;

                    default:
                        keyValue = new SZRegistryKey(string.Empty);
                        break;
                }

                entries.Add(new RegistryEntry()
                {
                    Name = valueName,
                    Data = keyValue,
                    Type = key.GetValueKind(valueName),
                    FullPath = key.Name + "\\" + valueName,
                    Location = key.Name
                });
            }

            return entries;
        }

        public static TestConnectionResult TestConnection(string machineName)
        {
            RegistryKey hive = null;
            try
            {
                hive = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machineName, RegistryView.Registry64);

                return new TestConnectionResult()
                {
                    Successful = true
                };
            }
            catch(Exception ex)
            {
                return new TestConnectionResult()
                {
                    Exception = ex,
                    Successful = false
                };
            }
            finally
            {
                hive?.Close();
            }
        }
        
    }

}

