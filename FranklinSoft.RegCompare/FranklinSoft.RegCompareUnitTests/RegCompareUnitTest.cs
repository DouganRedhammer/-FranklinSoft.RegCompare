using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using FranklinSoft.RegCompare;
using FranklinSoft.RegCompare.Models;
using FranklinSoft.RegCompare.Results;

namespace FranklinSoft.RegCompareUnitTests
{
    [TestClass]
    public class RegCompareUnitTest
    {
        [TestMethod]
        public void GetRegistryEntriesFailTest()
        {
            RegistryEntriesResult result = RegistryCompare.GetRegistryEntries(RegistryHive.CurrentUser, "somekey", "machinea");
            Assert.IsFalse(result.Successful);
        }

        [TestMethod]
        public void GetRegistryEntriesSuccessTest()
        {
            string machineName = System.Environment.MachineName;
            string rootKey = "Console";
            RegistryHive hive = RegistryHive.CurrentUser;
            RegistryEntriesResult result = RegistryCompare.GetRegistryEntries(RegistryHive.CurrentUser, rootKey, machineName);
            Assert.IsTrue(result.Successful);
        }

        [TestMethod]
        public void TestConnectionSuccessTest()
        {
            string machineName = System.Environment.MachineName;
            TestConnectionResult result = RegistryCompare.TestConnection(machineName);
            Assert.IsTrue(result.Successful);
        }

        [TestMethod]
        public void TestConnectionFailedTest()
        {
            TestConnectionResult result = RegistryCompare.TestConnection("machinea");
            Assert.IsFalse(result.Successful);
        }

        [TestMethod]
        public async Task TestConnectionAsyncSuccessTest()
        {
            string machineName = System.Environment.MachineName;
            TestConnectionResult result = await RegistryCompare.TestConnectionAsync(machineName);
            Assert.IsTrue(result.Successful);
        }

        [TestMethod]
        public async Task TestConnectionAsyncFailedTest()
        {
            TestConnectionResult result = await RegistryCompare.TestConnectionAsync("machinea");
            Assert.IsFalse(result.Successful);
        }

        [TestMethod]
        public async Task GetRegistryEntriesSuccessAsyncTest()
        {
            string machineName = System.Environment.MachineName;
            string rootKey = "Console";
            RegistryHive hive = RegistryHive.CurrentUser;
            RegistryEntriesResult result = await RegistryCompare.GetRegistryEntriesAsync(hive, rootKey, machineName);
            Assert.IsTrue(result.RegistryEntries.Count > 0, "That key does not have any entries.");
        }

        [TestMethod]
        public async Task GetRegistryEntriesFailedAsyncTest()
        {
            RegistryEntriesResult result = await RegistryCompare.GetRegistryEntriesAsync(RegistryHive.CurrentUser, "somekey", "machinea");
            Assert.AreEqual(0, result.RegistryEntries.Count);
        }

        [TestMethod]
        public async Task FindMatchingRegistryEntriesSZSuccessTest()
        {
            List<RegistryEntry> listA = new List<RegistryEntry>();
            List<RegistryEntry> listB = new List<RegistryEntry>();

            listA.Add(new RegistryEntry()
            {
                Data = new SZRegistryKey("testing1"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.String
                
            });

            listB.Add(new RegistryEntry()
            {
                Data = new SZRegistryKey("testing2"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.String

            });

            List<RegistryEntryDifference> matchingRegistryEntries = RegistryCompare.FindMatchingRegistryEntries(listA, listB);
            Assert.IsTrue(matchingRegistryEntries.Count > 0);
        }

        [TestMethod]
        public async Task FindMatchingRegistryEntriesSZEmptyTest()
        {
            List<RegistryEntry> listA = new List<RegistryEntry>();
            List<RegistryEntry> listB = new List<RegistryEntry>();

            listA.Add(new RegistryEntry()
            {
                Data = new SZRegistryKey("testing"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.String

            });

            listB.Add(new RegistryEntry()
            {
                Data = new SZRegistryKey("testing"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.String

            });

            List<RegistryEntryDifference> matchingRegistryEntries = RegistryCompare.FindMatchingRegistryEntries(listA, listB);
            Assert.IsTrue(matchingRegistryEntries.Count == 0);
        }

        [TestMethod]
        public async Task FindMatchingRegistryEntriesDWordSucessTest()
        {
            List<RegistryEntry> listA = new List<RegistryEntry>();
            List<RegistryEntry> listB = new List<RegistryEntry>();

            listA.Add(new RegistryEntry()
            {
                Data = new DwordRegistryKey(5),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.DWord

            });

            listB.Add(new RegistryEntry()
            {
                Data = new DwordRegistryKey(6),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.DWord

            });

            List<RegistryEntryDifference> matchingRegistryEntries = RegistryCompare.FindMatchingRegistryEntries(listA, listB);
            Assert.IsTrue(matchingRegistryEntries.Count > 0);
        }

        [TestMethod]
        public async Task FindMatchingRegistryEntriesDWordEmptyTest()
        {
            List<RegistryEntry> listA = new List<RegistryEntry>();
            List<RegistryEntry> listB = new List<RegistryEntry>();

            listA.Add(new RegistryEntry()
            {
                Data = new DwordRegistryKey(5),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.DWord

            });

            listB.Add(new RegistryEntry()
            {
                Data = new DwordRegistryKey(5),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.DWord

            });

            List<RegistryEntryDifference> matchingRegistryEntries = RegistryCompare.FindMatchingRegistryEntries(listA, listB);
            Assert.IsTrue(matchingRegistryEntries.Count == 0);
        }

        [TestMethod]
        public async Task FindMatchingRegistryEntriesBinarySucessTest()
        {
            List<RegistryEntry> listA = new List<RegistryEntry>();
            List<RegistryEntry> listB = new List<RegistryEntry>();

            listA.Add(new RegistryEntry()
            {
                Data = new BinaryRegistryKey("a"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.Binary

            });

            listB.Add(new RegistryEntry()
            {
                Data = new BinaryRegistryKey("b"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.Binary

            });

            List<RegistryEntryDifference> matchingRegistryEntries = RegistryCompare.FindMatchingRegistryEntries(listA, listB);
            Assert.IsTrue(matchingRegistryEntries.Count > 0);
        }

        [TestMethod]
        public async Task FindMatchingRegistryEntriesBinaryEmptyTest()
        {
            List<RegistryEntry> listA = new List<RegistryEntry>();
            List<RegistryEntry> listB = new List<RegistryEntry>();

            listA.Add(new RegistryEntry()
            {
                Data = new BinaryRegistryKey("a"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.Binary

            });

            listB.Add(new RegistryEntry()
            {
                Data = new BinaryRegistryKey("a"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.Binary

            });

            List<RegistryEntryDifference> matchingRegistryEntries = RegistryCompare.FindMatchingRegistryEntries(listA, listB);
            Assert.IsTrue(matchingRegistryEntries.Count == 0);
        }

        [TestMethod]
        public async Task FindMatchingRegistryEntriesQwordSucessTest()
        {
            List<RegistryEntry> listA = new List<RegistryEntry>();
            List<RegistryEntry> listB = new List<RegistryEntry>();

            listA.Add(new RegistryEntry()
            {
                Data = new QwordRegistryKey("a"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.Binary

            });

            listB.Add(new RegistryEntry()
            {
                Data = new QwordRegistryKey("b"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.Binary

            });

            List<RegistryEntryDifference> matchingRegistryEntries = RegistryCompare.FindMatchingRegistryEntries(listA, listB);
            Assert.IsTrue(matchingRegistryEntries.Count > 0);
        }

        [TestMethod]
        public async Task FindMatchingRegistryEntriesQwordEmptyTest()
        {
            List<RegistryEntry> listA = new List<RegistryEntry>();
            List<RegistryEntry> listB = new List<RegistryEntry>();

            listA.Add(new RegistryEntry()
            {
                Data = new QwordRegistryKey("a"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.Binary

            });

            listB.Add(new RegistryEntry()
            {
                Data = new QwordRegistryKey("a"),
                FullPath = "a",
                Location = "a\\a",
                Name = "testkey",
                Type = RegistryValueKind.Binary

            });

            List<RegistryEntryDifference> matchingRegistryEntries = RegistryCompare.FindMatchingRegistryEntries(listA, listB);
            Assert.IsTrue(matchingRegistryEntries.Count == 0);
        }

        [TestMethod]
        public async Task TestConnectionAsyncTimeoutTest()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            int timeout = 100000000;
            string machineName = System.Environment.MachineName;
            TestConnectionResult result = await RegistryCompare.TestConnectionAsync(machineName, cancellationTokenSource, cancellationToken, timeout);
            Assert.IsTrue(result.Successful);
        }

        [TestMethod]
        public async Task GetRegistryEntriesSuccessAsyncNoResultsTest()
        {
            string machineName = System.Environment.MachineName;
            string rootKey = "asdgf3323";
            RegistryHive hive = RegistryHive.CurrentUser;
            RegistryEntriesResult result = await RegistryCompare.GetRegistryEntriesAsync(hive, rootKey, machineName);
            Assert.IsTrue(result.RegistryEntries.Count == 0, "That key does not have any entries.");
        }
    }
}
