using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESNLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ESNLib.Tools.UnitTests
{
    [TestClass()]
    public class LoggerStreamTests
    {
        readonly string path = Logger.GetDefaultLogPath("ESN", "UnitTests", "log.txt");

        readonly string pathStream = Logger.GetDefaultLogPath("ESN", "UnitTests", "log_stream.txt");

        /// <summary>
        /// Delete UnitTests directory to perform a clean test
        /// </summary>
        private void DeleteDirectory()
        {
            if (Directory.Exists(Path.GetDirectoryName(path)))
                Directory.Delete(Path.GetDirectoryName(path), true);
        }

        [TestMethod()]
        public void LoggerFileAndStreamTest()
        {
            DeleteDirectory();

            if (!Logger.CheckFilePath(pathStream))
                Assert.Fail("Unable to validate path");

            StreamWriter sw = new StreamWriter(pathStream);
            StreamLogger<StreamWriter> sl = new StreamLogger<StreamWriter>(sw);


            LoggerStream<StreamWriter> log = new LoggerStream<StreamWriter>
            {
                OutputStream = sl,
                FilePath = path,
                WriteMode = Logger.WriteModes.Stream | Logger.WriteModes.Write,
                FilenameMode = Logger.FilenamesModes.FileName,
                PrefixMode = Logger.PrefixModes.None
            };


            Assert.IsTrue(log.Enable());
            string outputPath = log.FileOutputPath;

            Assert.IsTrue(log.Write("Hello world"));
            log.Dispose();


            DeleteDirectory();
        }

        [TestMethod()]
        public void LoggerStreamTest()
        {
            DeleteDirectory();

            if (!Logger.CheckFilePath(pathStream))
                Assert.Fail("Unable to validate path");

            StreamWriter sw = new StreamWriter(pathStream);
            StreamLogger<StreamWriter> sl = new StreamLogger<StreamWriter>(sw);

            LoggerStream<StreamWriter> log = new LoggerStream<StreamWriter>()
            {
                OutputStream = sl,
                FilePath = path,
                WriteMode = Logger.WriteModes.Stream,
                FilenameMode = Logger.FilenamesModes.FileName,
                PrefixMode = Logger.PrefixModes.None,
            };

            Assert.IsTrue(log.Enable());
            string outputPath = log.FileOutputPath;

            Assert.IsTrue(log.Write("Hello world"));
            log.Dispose();

            string dataStream = File.ReadAllText(pathStream).Trim();

            Assert.IsFalse(File.Exists(outputPath));
            Assert.AreEqual("[Debug] Hello world", dataStream);

            DeleteDirectory();
        }

        [TestMethod()]
        public void LoggerFileTest()
        {
            DeleteDirectory();

            LoggerTests lg = new LoggerTests();
            lg.BasicAppendLastPreviousTest();
            lg.BasicAppendTest();
            lg.BasicWriteTest();
            lg.FileNameLastPreviousTest();
            lg.FileNameTest();
            lg.FileNameDateSuffixTest();
            lg.LoggerBasicTest();
            lg.LoggerCurrentTimePrefixTest();
            lg.LoggerNoPrefixTest();
            lg.LoggerRuntimePrefixTest();
            lg.LoggerCustomPrefixTest();

            DeleteDirectory();
        }
    }
}