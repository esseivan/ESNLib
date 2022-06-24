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
        readonly string path = Path.Combine(Logger.GetDefaultLogPath("ESN", "UnitTests"), "log.txt");

        readonly string pathStream = Path.Combine(Logger.GetDefaultLogPath("ESN", "UnitTests"), "log_stream.txt");

        [TestMethod()]
        public void LoggerFileAndStreamTest()
        {
            StreamWriter sw = new StreamWriter(pathStream);
            StreamLogger<StreamWriter> sl = new StreamLogger<StreamWriter>(sw);

            LoggerStream<StreamWriter> log = new LoggerStream<StreamWriter>()
            {
                OutputStream = sl,
                FilePath = path,
                WriteMode = Logger.WriteModes.Stream | Logger.WriteModes.Write,
                FilenameMode = Logger.FilenamesModes.FileName,
                PrefixMode = Logger.PrefixModes.None,
            };

            Assert.IsTrue(log.Enable());
            string outputPath = log.FileOutputPath;

            Assert.IsTrue(log.Write("Hello world"));
            log.Dispose();


        }

        [TestMethod()]
        public void LoggerStreamTest()
        {
            StreamWriter sw = new StreamWriter(pathStream);
            StreamLogger<StreamWriter> sl = new StreamLogger<StreamWriter>(sw);

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
        }

        [TestMethod()]
        public void LoggerFileTest()
        {
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
        }
    }
}