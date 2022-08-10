using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESNLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ESNLib.Tools.UnitTests
{
    [TestClass()]
    public class LoggerTests
    {
        readonly string path = Logger.GetDefaultLogPath("ESN", "UnitTests", "log.txt");

        [TestMethod()]
        public void LoggerRuntimePrefixTest()
        {
            Logger log = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName_LastPrevious,
                FilePath = path,
                PrefixMode = Logger.PrefixModes.RunTime,
                WriteMode = Logger.WriteModes.Write,
            };

            Assert.IsTrue(log.Enable());

            string outputPath = log.FileOutputPath;

            Assert.IsTrue(log.Write("Hello world"));

            string data = File.ReadAllText(outputPath).Trim();
            StringBuilder sb = new StringBuilder(data);
            sb[10] = '0';
            data = sb.ToString();

            Assert.AreEqual("[000000.000] [Debug] Hello world", data);
        }

        [TestMethod()]
        public void LoggerCurrentTimePrefixTest()
        {
            Logger log = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName_LastPrevious,
                FilePath = path,
                PrefixMode = Logger.PrefixModes.CurrentTime,
                WriteMode = Logger.WriteModes.Write,
                CurrentTimePrefixFormat = "HH:mm:ss.f",
            };

            Assert.IsTrue(log.Enable());

            string outputPath = log.FileOutputPath;

            string prefix = DateTime.Now.ToString(log.CurrentTimePrefixFormat);
            Assert.IsTrue(log.Write("Hello world"));

            string data = File.ReadAllText(outputPath).Trim();

            // Only works if not too many miliseconds are displayed
            Assert.AreEqual($"[{prefix}] [Debug] Hello world", data);
        }

        [TestMethod()]
        public void LoggerCustomPrefixTest()
        {
            Logger log = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName_LastPrevious,
                FilePath = path,
                PrefixMode = Logger.PrefixModes.Custom,
                WriteMode = Logger.WriteModes.Write,
                CustomPrefix = "Server"
            };

            Assert.IsTrue(log.Enable());

            string outputPath = log.FileOutputPath;

            Assert.IsTrue(log.Write("Hello world"));

            string data = File.ReadAllText(outputPath).Trim();

            // Only works if not too many miliseconds are displayed
            Assert.AreEqual($"[{log.CustomPrefix}] [Debug] Hello world", data);
        }

        [TestMethod()]
        public void LoggerNoPrefixTest()
        {
            Logger log = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName_LastPrevious,
                FilePath = path,
                PrefixMode = Logger.PrefixModes.None,
                WriteMode = Logger.WriteModes.Write,
            };

            Assert.IsTrue(log.Enable());

            string outputPath = log.FileOutputPath;

            Assert.IsTrue(log.Write("Hello world"));

            string data = File.ReadAllText(outputPath).Trim();

            // Only works if not too many miliseconds are displayed
            Assert.AreEqual($"[Debug] Hello world", data);
        }

        [TestMethod()]
        public void LoggerBasicTest()
        {
            Logger log = new Logger(path)
            {
                WriteMode = Logger.WriteModes.Write
            };

            Assert.IsTrue(log.Enable());

            string outputPath = log.FileOutputPath;

            Assert.IsTrue(log.Write("Hello world"));

            string data = File.ReadAllText(outputPath).Trim();
            StringBuilder sb = new StringBuilder(data);
            sb[10] = '0';
            data = sb.ToString();

            Assert.AreEqual("[000000.000] [Debug] Hello world", data);
        }

        [TestMethod()]
        public void FileNameLastPreviousTest()
        {
            Logger log = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName_LastPrevious,
                FilePath = path,
                WriteMode = Logger.WriteModes.Write,
                PrefixMode = Logger.PrefixModes.None,
            };

            Assert.IsTrue(log.Enable());
            Assert.IsTrue(log.Write("Hello world 1"));
            string outputPath1 = log.FileOutputPath;
            string data1 = File.ReadAllText(outputPath1).Trim();

            // 2nd write
            log.Disable();
            Assert.IsTrue(log.Enable());
            Assert.IsTrue(log.Write("Hello world 2"));
            string outputPath2 = log.FileOutputPath;
            string data2 = File.ReadAllText(outputPath2).Trim();


            Assert.AreEqual("[Debug] Hello world 1", data1);
            Assert.AreEqual("log_current.txt", Path.GetFileName(outputPath1));
            Assert.AreEqual("[Debug] Hello world 2", data2);
            Assert.AreEqual("log_current.txt", Path.GetFileName(outputPath2));
        }

        [TestMethod()]
        public void FileNameDateSuffixTest()
        {
            Logger log = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName_DateSuffix,
                FilePath = path,
                WriteMode = Logger.WriteModes.Write,
                PrefixMode = Logger.PrefixModes.None,
            };

            string suffix = DateTime.Now.ToString(log.DateSuffixFormat);
            Assert.IsTrue(log.Enable());
            Assert.IsTrue(log.Write("Hello world"));
            string outputPath = log.FileOutputPath;
            string data = File.ReadAllText(outputPath).Trim();

            Assert.AreEqual("[Debug] Hello world", data);
            Assert.AreEqual("log_" + suffix + ".txt", Path.GetFileName(outputPath));
        }

        [TestMethod()]
        public void FileNameTest()
        {
            Logger log = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName,
                FilePath = path,
                WriteMode = Logger.WriteModes.Write,
                PrefixMode = Logger.PrefixModes.None,
            };

            string suffix = DateTime.Now.ToString(log.DateSuffixFormat);
            Assert.IsTrue(log.Enable());
            Assert.IsTrue(log.Write("Hello world"));
            string outputPath = log.FileOutputPath;
            string data = File.ReadAllText(outputPath).Trim();

            Assert.AreEqual("[Debug] Hello world", data);
            Assert.AreEqual("log.txt", Path.GetFileName(outputPath));
        }

        [TestMethod()]
        public void BasicWriteTest()
        {
            Logger log = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName,
                FilePath = path,
                WriteMode = Logger.WriteModes.Write,
                PrefixMode = Logger.PrefixModes.None,
            };

            Assert.IsTrue(log.Enable());
            Assert.IsTrue(log.Write("Hello world 1"));
            string outputPath1 = log.FileOutputPath;
            string data1 = File.ReadAllText(outputPath1).Trim();

            // 2nd write
            log.Disable();
            Assert.IsTrue(log.Enable());
            Assert.IsTrue(log.Write("Hello world 2"));
            string outputPath2 = log.FileOutputPath;
            string data2 = File.ReadAllText(outputPath2).Trim();


            Assert.AreEqual(outputPath1, outputPath2);
            Assert.AreEqual("[Debug] Hello world 2", data2);
        }

        [TestMethod()]
        public void BasicAppendTest()
        {
            Logger logDel = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName,
                FilePath = path,
                WriteMode = Logger.WriteModes.Write,
                PrefixMode = Logger.PrefixModes.None,
            };
            string output = logDel.CheckFile(path);
            logDel.Dispose();
            File.Delete(output);


            Logger log = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName,
                FilePath = path,
                WriteMode = Logger.WriteModes.Append,
                PrefixMode = Logger.PrefixModes.None,
            };

            Assert.IsTrue(log.Enable());
            Assert.IsTrue(log.Write("Hello world 1"));
            string outputPath1 = log.FileOutputPath;
            string data1 = File.ReadAllText(outputPath1).Trim();

            // 2nd write
            log.Disable();
            Assert.IsTrue(log.Enable());
            Assert.IsTrue(log.Write("Hello world 2"));
            string outputPath2 = log.FileOutputPath;
            string data2 = File.ReadAllText(outputPath2).Trim();


            Assert.AreEqual(outputPath1, outputPath2);
            Assert.AreEqual("[Debug] Hello world 1" + Environment.NewLine + "[Debug] Hello world 2", data2);
        }

        [TestMethod()]
        public void BasicAppendLastPreviousTest()
        {
            Logger log = new Logger()
            {
                FilenameMode = Logger.FilenamesModes.FileName_LastPrevious,
                FilePath = path,
                WriteMode = Logger.WriteModes.Append,
                PrefixMode = Logger.PrefixModes.None,
            };

            Assert.IsTrue(log.Enable());
            Assert.IsTrue(log.Write("Hello world 1"));
            string outputPath1 = log.FileOutputPath;
            string data1 = File.ReadAllText(outputPath1).Trim();

            // 2nd write
            log.Disable();
            Assert.IsTrue(log.Enable());
            Assert.IsTrue(log.Write("Hello world 2"));
            string outputPath2 = log.FileOutputPath;
            string data2 = File.ReadAllText(outputPath2).Trim();

            // Equal because both are current
            Assert.AreEqual(outputPath1, outputPath2);
            Assert.AreEqual("[Debug] Hello world 2", data2);
        }
    }
}