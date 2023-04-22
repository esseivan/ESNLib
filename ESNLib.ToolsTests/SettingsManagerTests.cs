using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESNLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ESNLib.Tools.Tests
{
    [TestClass()]
    public class SettingsManagerTests
    {
        private static string DEFAULT_APP_NAME = "UnitTesting";

        /// <summary>
        /// Required to have different files names, otherwise tests might fail if they try to edit the same file
        /// </summary>
        private string GetTestName(int index)
        {
            string value = $"{DEFAULT_APP_NAME}{index}";
            SettingsManager.MyAppName = value;
            return value;
        }

        [TestMethod()]
        public void TestSaveAndLoadFromDefault()
        {
            _ = GetTestName(1);

            // Save file then load it and read content
            SettingsManager.SaveToDefault(Math.PI, SettingsManager.BackupMode.None, false);

            SettingsManager.LoadFromDefault(out double read);

            // Save one more time for backup
            SettingsManager.SaveToDefault(Math.PI, SettingsManager.BackupMode.None, false);

            Assert.IsTrue(read == Math.PI);
        }

        [TestMethod()]
        public void TestSaveAndLoadArrayFromDefault()
        {
            _ = GetTestName(2);

            // Save file then load it and read content
            SettingsManager.SaveToDefault(
                new double[] { Math.PI, Math.E },
                SettingsManager.BackupMode.None,
                false
            );

            SettingsManager.LoadFromDefault(out double[] read);

            // Save one more time for backup
            SettingsManager.SaveToDefault(
                new double[] { Math.PI, Math.E },
                SettingsManager.BackupMode.None,
                false
            );

            Assert.IsTrue(read.Length == 2);
            Assert.IsTrue(read[0] == Math.PI);
            Assert.IsTrue(read[1] == Math.E);
        }

        [TestMethod()]
        public void TestSaveAndLoadArrayFromPath()
        {
            _ = GetTestName(3);
            string path = SettingsManager.GetDefaultSettingPath(false);

            Console.WriteLine(path);
            // Save file then load it and read content
            SettingsManager.SaveTo(
                path,
                new double[] { Math.PI, Math.E },
                SettingsManager.BackupMode.None,
                false
            );

            SettingsManager.LoadFrom(path, out double[] read);

            // Save one more time for backup
            SettingsManager.SaveTo(
                path,
                new double[] { Math.PI, Math.E },
                SettingsManager.BackupMode.None,
                false
            );

            Assert.IsTrue(read.Length == 2);
            Assert.IsTrue(read[0] == Math.PI);
            Assert.IsTrue(read[1] == Math.E);
        }

        [TestMethod()]
        public void TestSaveAndLoadCustomClassFromDefault()
        {
            _ = GetTestName(4);

            TestClass tc = new TestClass()
            {
                x = -1,
                name = "Hello world !",
                constants = new double[] { Math.PI, Math.E },
                hiddenText = "password",
            };

            // Save file then load it and read content
            SettingsManager.SaveToDefault(tc, SettingsManager.BackupMode.None, false);

            SettingsManager.LoadFromDefault(out TestClass read);

            // Save one more time for backup
            SettingsManager.SaveToDefault(tc, SettingsManager.BackupMode.None, false);

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
            _ = GetTestName(5);

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
            SettingsManager.SaveToDefault(
                new TestClass[] { tc1, tc2 },
                SettingsManager.BackupMode.None,
                false
            );

            SettingsManager.LoadFromDefault(out TestClass[] read);

            // Save one more time for backup
            SettingsManager.SaveToDefault(
                new TestClass[] { tc1, tc2 },
                SettingsManager.BackupMode.None,
                false
            );

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
        public void TestSaveAndLoadCustomClassArrayFromDefaultHide()
        {
            _ = GetTestName(6);

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
            SettingsManager.SaveToDefault(
                new TestClass[] { tc1, tc2 },
                backup: SettingsManager.BackupMode.None,
                indent: false,
                hide: true
            );

            SettingsManager.LoadFromDefault(out TestClass[] read);

            // Save one more time for backup
            SettingsManager.SaveToDefault(
                new TestClass[] { tc1, tc2 },
                backup: SettingsManager.BackupMode.None,
                indent: false,
                hide: true
            );

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
        public void TestSaveAndLoadCustomClassArrayFromDefaultBackup()
        {
            _ = GetTestName(7);

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
            SettingsManager.SaveToDefault(
                new TestClass[] { tc1, tc2 },
                backup: SettingsManager.BackupMode.None,
                indent: false,
                hide: false
            );

            SettingsManager.LoadFromDefault(out TestClass[] read);

            // Save one more time for backup
            SettingsManager.SaveToDefault(
                new TestClass[] { tc1, tc2 },
                backup: SettingsManager.BackupMode.None,
                indent: false,
                hide: false
            );

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
        public void TestSaveAndLoadCustomClassArrayFromDefaultBackupHide()
        {
            _ = GetTestName(8);

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
            SettingsManager.SaveToDefault(
                new TestClass[] { tc1, tc2 },
                backup: SettingsManager.BackupMode.None,
                indent: false,
                hide: true
            );

            SettingsManager.LoadFromDefault(out TestClass[] read);

            // Save one more time for backup
            SettingsManager.SaveToDefault(
                new TestClass[] { tc1, tc2 },
                backup: SettingsManager.BackupMode.None,
                indent: false,
                hide: true
            );

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
            _ = GetTestName(9);

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
            List<TestClass> list = new List<TestClass>() { tc1, tc2 };

            // Save file then load it and read content
            SettingsManager.SaveToDefault(list, SettingsManager.BackupMode.None, false);

            SettingsManager.LoadFromDefault(out List<TestClass> read);

            // Save one more time for backup
            SettingsManager.SaveToDefault(list, SettingsManager.BackupMode.None, false);

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
            _ = GetTestName(10);
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
            SettingsManager.SaveToDefault(dic, indent: true);

            SettingsManager.LoadFromDefault(out Dictionary<string, TestClass> read);

            // Save one more time for backup
            SettingsManager.SaveToDefault(dic, indent: true);

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

        [TestMethod()]
        public void TestSaveAndLoadCustomClassDictionaryFromDefault_NonZipped()
        {
            _ = GetTestName(11);
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
            SettingsManager.SaveToDefault(dic, indent: true, zipFile: false);

            SettingsManager.LoadFromDefault(out Dictionary<string, TestClass> read, zipFile: false);

            // Save one more time for backup
            SettingsManager.SaveToDefault(dic, indent: true, zipFile: false);

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

        [TestMethod()]
        public void TestSaveAndLoadCustomClassDictionaryFromDefault_Zipped()
        {
            _ = GetTestName(12);
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
            SettingsManager.SaveToDefault(dic, indent: true, zipFile: true);

            SettingsManager.LoadFromDefault(out Dictionary<string, TestClass> read, zipFile: true);

            // Save one more time for backup
            SettingsManager.SaveToDefault(dic, indent: true, zipFile: true);

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

        [TestMethod()]
        public void TestSaveAndLoadCustomClassDictionaryFromDefault_BackupDateFormat()
        {
            _ = GetTestName(13);
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
            SettingsManager.SaveToDefault(
                dic,
                indent: true,
                zipFile: false,
                backup: SettingsManager.BackupMode.datetimeFormatAppdata
            );

            SettingsManager.LoadFromDefault(out Dictionary<string, TestClass> read, zipFile: false);

            // Save one more time for backup
            SettingsManager.SaveToDefault(
                dic,
                indent: true,
                zipFile: false,
                backup: SettingsManager.BackupMode.datetimeFormatAppdata
            );

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

        public class TestClass
        {
            [JsonPropertyName("Value")]
            public int x { get; set; }
            public string name { get; set; }

            [JsonNumberHandling(JsonNumberHandling.AllowNamedFloatingPointLiterals)]
            public double[] constants { get; set; }

            [JsonIgnore()]
            public string hiddenText { get; set; }
        }
    }
}
