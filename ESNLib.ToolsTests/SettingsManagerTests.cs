using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESNLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools.Tests
{
    [TestClass()]
    public class SettingsManagerTests
    {
        private static string AppName = "UnitTesting";

        [TestMethod()]
        public void TestSaveAndLoadFromDefault()
        {
            // Save file then load it and read content
            SettingsManager.SaveToDefault(AppName, Math.PI, false, false);

            SettingsManager.LoadFromDefault(AppName, out double read);

            Assert.IsTrue(read == Math.PI);
        }

        [TestMethod()]
        public void TestSaveAndLoadArrayFromDefault()
        {
            // Save file then load it and read content
            SettingsManager.SaveToDefault(AppName, new double[] { Math.PI, Math.E }, false, false);

            SettingsManager.LoadFromDefault(AppName, out double[] read);

            Assert.IsTrue(read.Length == 2);
            Assert.IsTrue(read[0] == Math.PI);
            Assert.IsTrue(read[1] == Math.E);
        }

        [TestMethod()]
        public void TestSaveAndLoadArrayFromPath()
        {
            string path = SettingsManager.GetDefaultPath(AppName);

            // Save file then load it and read content
            SettingsManager.SaveTo(path, new double[] { Math.PI, Math.E }, false, false);

            SettingsManager.LoadFrom(path, out double[] read);

            Assert.IsTrue(read.Length == 2);
            Assert.IsTrue(read[0] == Math.PI);
            Assert.IsTrue(read[1] == Math.E);
        }

        [TestMethod()]
        public void TestSaveAndLoadCustomClassFromDefault()
        {
            TestClass tc = new TestClass()
            {
                x = -1,
                name = "Hello world !",
                constants = new double[] { Math.PI, Math.E },
                hiddenText = "password",
            };

            // Save file then load it and read content
            SettingsManager.SaveToDefault(AppName, tc, false, false);

            SettingsManager.LoadFromDefault(AppName, out TestClass read);

            Assert.AreEqual(tc.x, read.x);
            Assert.AreEqual(tc.name, read.name);
            Assert.AreEqual(tc.constants.Length, read.constants.Length);
            Assert.AreEqual(tc.constants[0], read.constants[0]);
            Assert.AreEqual(tc.constants[1], read.constants[1]);
            Assert.IsTrue(string.IsNullOrEmpty(read.hiddenText));
            Assert.AreNotEqual(tc.hiddenText, read.hiddenText);
        }

        [TestMethod()]
        public void TestSaveAndLoadCustomClassArrayFromDefault()
        {
            TestClass tc1 = new TestClass()
            {
                x = -1,
                name = "Hello world !",
                constants = new double[] { Math.PI, Math.E },
                hiddenText = "password",
            };
            TestClass tc2 = new TestClass()
            {
                x = -255,
                name = "Hello !",
                constants = new double[] { Double.NegativeInfinity, int.MaxValue },
                hiddenText = "123456",
            };

            // Save file then load it and read content
            SettingsManager.SaveToDefault(AppName, new TestClass[] { tc1, tc2 }, false, false);

            SettingsManager.LoadFromDefault(AppName, out TestClass[] read);

            Assert.AreEqual(2, read.Length);
            TestClass read1 = read[0];
            Assert.AreEqual(tc1.x, read1.x);
            Assert.AreEqual(tc1.name, read1.name);
            Assert.AreEqual(tc1.constants.Length, read1.constants.Length);
            Assert.AreEqual(tc1.constants[0], read1.constants[0]);
            Assert.AreEqual(tc1.constants[1], read1.constants[1]);
            Assert.IsTrue(string.IsNullOrEmpty(read1.hiddenText));
            Assert.AreNotEqual(tc1.hiddenText, read1.hiddenText);

            TestClass read2 = read[1];
            Assert.AreEqual(tc2.x, read2.x);
            Assert.AreEqual(tc2.name, read2.name);
            Assert.AreEqual(tc2.constants.Length, read2.constants.Length);
            Assert.AreEqual(tc2.constants[0], read2.constants[0]);
            Assert.AreEqual(tc2.constants[1], read2.constants[1]);
            Assert.IsTrue(string.IsNullOrEmpty(read2.hiddenText));
            Assert.AreNotEqual(tc2.hiddenText, read2.hiddenText);
        }

        [TestMethod()]
        public void TestSaveAndLoadCustomClassListFromDefault()
        {
            TestClass tc1 = new TestClass()
            {
                x = -1,
                name = "Hello world !",
                constants = new double[] { Math.PI, Math.E },
                hiddenText = "password",
            };
            TestClass tc2 = new TestClass()
            {
                x = -255,
                name = "Hello !",
                constants = new double[] { Double.NegativeInfinity, int.MaxValue },
                hiddenText = "123456",
            };
            List<TestClass> list = new List<TestClass>()
            {
                tc1,tc2
            };

            // Save file then load it and read content
            SettingsManager.SaveToDefault(AppName, list, false, false);

            SettingsManager.LoadFromDefault(AppName, out List<TestClass> read);

            Assert.AreEqual(2, read.Count);
            TestClass read1 = read[0];
            Assert.AreEqual(tc1.x, read1.x);
            Assert.AreEqual(tc1.name, read1.name);
            Assert.AreEqual(tc1.constants.Length, read1.constants.Length);
            Assert.AreEqual(tc1.constants[0], read1.constants[0]);
            Assert.AreEqual(tc1.constants[1], read1.constants[1]);
            Assert.IsTrue(string.IsNullOrEmpty(read1.hiddenText));
            Assert.AreNotEqual(tc1.hiddenText, read1.hiddenText);

            TestClass read2 = read[1];
            Assert.AreEqual(tc2.x, read2.x);
            Assert.AreEqual(tc2.name, read2.name);
            Assert.AreEqual(tc2.constants.Length, read2.constants.Length);
            Assert.AreEqual(tc2.constants[0], read2.constants[0]);
            Assert.AreEqual(tc2.constants[1], read2.constants[1]);
            Assert.IsTrue(string.IsNullOrEmpty(read2.hiddenText));
            Assert.AreNotEqual(tc2.hiddenText, read2.hiddenText);
        }

        [TestMethod()]
        public void TestSaveAndLoadCustomClassDictionaryFromDefault()
        {
            TestClass tc1 = new TestClass()
            {
                x = -1,
                name = "Hello world !",
                constants = new double[] { Math.PI, Math.E },
                hiddenText = "password",
            };
            TestClass tc2 = new TestClass()
            {
                x = -255,
                name = "Hello !",
                constants = new double[] { double.NegativeInfinity, int.MaxValue },
                hiddenText = "123456",
            };
            Dictionary<string, TestClass> dic = new Dictionary<string, TestClass>
            {
                { "Entry #1", tc1 },
                { "Entry #2", tc2 }
            };

            // Save file then load it and read content
            SettingsManager.SaveToDefault(AppName, dic, indent: true);

            SettingsManager.LoadFromDefault(AppName, out Dictionary<string, TestClass> read);

            Assert.AreEqual(2, read.Count);
            TestClass read1 = read["Entry #1"];
            Assert.AreEqual(tc1.x, read1.x);
            Assert.AreEqual(tc1.name, read1.name);
            Assert.AreEqual(tc1.constants.Length, read1.constants.Length);
            Assert.AreEqual(tc1.constants[0], read1.constants[0]);
            Assert.AreEqual(tc1.constants[1], read1.constants[1]);
            Assert.IsTrue(string.IsNullOrEmpty(read1.hiddenText));
            Assert.AreNotEqual(tc1.hiddenText, read1.hiddenText);

            TestClass read2 = read["Entry #2"];
            Assert.AreEqual(tc2.x, read2.x);
            Assert.AreEqual(tc2.name, read2.name);
            Assert.AreEqual(tc2.constants.Length, read2.constants.Length);
            Assert.AreEqual(tc2.constants[0], read2.constants[0]);
            Assert.AreEqual(tc2.constants[1], read2.constants[1]);
            Assert.IsTrue(string.IsNullOrEmpty(read2.hiddenText));
            Assert.AreNotEqual(tc2.hiddenText, read2.hiddenText);
        }

        private class TestClass
        {
            public int x;
            public string name;
            public double[] constants;
            [System.Text.Json.Serialization.JsonIgnore()]
            public string hiddenText;
        }
    }
}