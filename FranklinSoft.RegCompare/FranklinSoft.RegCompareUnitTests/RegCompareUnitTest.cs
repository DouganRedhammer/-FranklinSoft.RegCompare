using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using FranklinSoft.RegCompare;
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
        public async Task GetRegistryEntriesSuccessAsyncCancellationTest()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            string machineName = System.Environment.MachineName;
            string rootKey = "Console";
            RegistryHive hive = RegistryHive.CurrentUser;
            int timeout = 1;
            RegistryEntriesResult result = await RegistryCompare.GetRegistryEntriesAsync(hive, rootKey, machineName,tokenSource,token, timeout);
            Assert.IsFalse(!result.Successful);
        }
    }
}
