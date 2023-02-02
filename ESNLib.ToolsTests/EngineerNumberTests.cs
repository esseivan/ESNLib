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
    public class EngineerNumberTests
    {
        [TestMethod()]
        public void EngineerNumberTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DecimalToEngineerTest()
        {
            EngineerNumber number1 = new EngineerNumber(2.33e-9, "A", true, 3);
            EngineerNumber number2 = new EngineerNumber(2.33e-44, "L", true, 3);
            EngineerNumber number3 = new EngineerNumber(233e-44, "N", true, 3);
            EngineerNumber number4 = new EngineerNumber(2.33e9, "T", true, 3);
            EngineerNumber number5 = new EngineerNumber(2.33e44, "cad", true, 3);
            EngineerNumber number6 = new EngineerNumber(233e44, "lux", true, 3);

            string output1 = number1.ToString();
            string output2 = number2.ToString();
            string output3 = number3.ToString();
            string output4 = number4.ToString();
            string output5 = number5.ToString();
            string output6 = number6.ToString();

            Assert.AreEqual("2.33 nA", output1);
            Assert.AreEqual("23.3e-45 L", output2);
            Assert.AreEqual("2.33e-42 N", output3);
            Assert.AreEqual("2.33 GT", output4);
            Assert.AreEqual("233e42 cad", output5);
            Assert.AreEqual("23.3e45 lux", output6);
        }

        [TestMethod()]
        public void EngineerToDecimalTest()
        {
            Assert.Fail();
        }
    }
}
