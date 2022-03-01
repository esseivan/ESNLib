using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESNLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ESNLib.Tools.Tests
{
    [TestClass()]
    public class LoggerStreamTests
    {
        readonly string path = Path.Combine(Logger.GetDefaultLogPath("ESN", "UnitTests"), "log.txt");

        readonly string pathStream = Path.Combine(Logger.GetDefaultLogPath("ESN", "UnitTests"), "log_stream.txt");

        [TestMethod()]
        public void LoggerStreamTest()
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
        public void CheckFileTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteTest()
        {
            Assert.Fail();
        }
    }
}