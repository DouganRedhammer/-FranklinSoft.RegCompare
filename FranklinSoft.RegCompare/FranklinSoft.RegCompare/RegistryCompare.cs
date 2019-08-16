using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FranklinSoft.RegCompare.Models;
using FranklinSoft.RegCompare.Results;
using Microsoft.Win32;
using RegistryKey = Microsoft.Win32.RegistryKey;

namespace FranklinSoft.RegCompare
{
    public static class RegistryCompare
    {
        #region Public Methods

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

        public static List<RegistryEntry> FindMissingRegistryEntries(List<RegistryEntry> list1, List<RegistryEntry> list2)
        {
            return list1.Except(list2, new MissingRegistryEntryComparer()).ToList();
        }

        public static List<RegistryEntryDifference> FindMatchingRegistryEntries(List<RegistryEntry> list1, List<RegistryEntry> list2)
        {
            return (from A in list1
                join B in list2 on A.Name.ToLower() equals B.Name.ToLower()
                where !A.Data.Equals(B.Data)
                select new RegistryEntryDifference()
                {
                    Name = A.Name,
                    Location = A.Location,
                    Type = A.Type,
                    MachineAData = A.Data,
                    MachineBData = B.Data
                }).ToList();
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
                    Successful = false,
                    Message =  ex.Message
                };
            }
            finally
            {
                hive?.Close();
            }
        }

        public static async Task<TestConnectionResult> TestConnectionAsync(string machineName, CancellationTokenSource tokenSource, CancellationToken token, int timeout)
        {
            Task<bool> testConnection = Task<bool>.Factory.StartNew(() =>
            {
                TestConnectionResult result = TestConnection(machineName);
                return result.Successful;
            }, tokenSource.Token);

            TestConnectionResult testConnectionResult = new TestConnectionResult()
            {
                Message = string.Empty,
                Successful = false
            };

            if (await Task.WhenAny(testConnection, Task.Delay(timeout, token)) == testConnection)
            {
                await testConnection;
                testConnectionResult.Successful = testConnection.Result;
                return testConnectionResult;
            }

            if (!testConnection.Result)
            {
                try
                {
                    tokenSource.Cancel();
                    token.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException ex)
                {
                    testConnectionResult.Exception = ex;
                    return testConnectionResult;
                }
            }

            return testConnectionResult;
        }

        public static async Task<TestConnectionResult> TestConnectionAsync(string machineName)
        {
            Task<bool> testConnection = Task<bool>.Factory.StartNew(() =>
            {
                TestConnectionResult result = TestConnection(machineName);
                return result.Successful;
            });

            TestConnectionResult testConnectionResult = new TestConnectionResult()
            {
                Message = string.Empty,
                Successful = false
            };

                await testConnection;
                testConnectionResult.Successful = testConnection.Result;
                return testConnectionResult;
        }

        public static async Task<RegistryEntriesResult> GetRegistryEntriesAsync(RegistryHive registryHive, string rootKey, string machineName)
        {
            Task<RegistryEntriesResult> regKeysMachine = Task<RegistryEntriesResult>.Factory.StartNew(() =>
            {
                RegistryEntriesResult result = GetRegistryEntries(registryHive, rootKey, machineName);
                return result;
            });

            await regKeysMachine;
            return regKeysMachine.Result;
        }

        public static async Task<RegistryEntriesResult> GetRegistryEntriesAsync(RegistryHive registryHive, string rootKey, string machineName, CancellationTokenSource tokenSource, CancellationToken token, int timeout)
        {
            Task<RegistryEntriesResult> regKeysMachine = Task<RegistryEntriesResult>.Factory.StartNew(() =>
            {
                RegistryEntriesResult result = GetRegistryEntries(registryHive, rootKey, machineName);
                return result;
            }, tokenSource.Token);
            if (await Task.WhenAny(regKeysMachine, Task.Delay(timeout, token)) == regKeysMachine)
            {
                await regKeysMachine;
                return regKeysMachine.Result;
            }
            else
            {
                try
                {
                    tokenSource.Cancel();
                    token.ThrowIfCancellationRequested();
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
            }

            return new RegistryEntriesResult()
            {
                Successful = true,
                RegistryEntries = new List<RegistryEntry>()
            };
        }

        #endregion

        #region Private Methods

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
        #endregion
    }
}

