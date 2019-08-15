using System;
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
    }
}
