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
        public void DecimalToEngineerTest_precision3_space()
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
        public void DecimalToEngineerTest_precision2_nospace()
        {
            EngineerNumber number1 = new EngineerNumber(2.33e-9, "A", false, 2);
            EngineerNumber number2 = new EngineerNumber(2.33e-44, "L", false, 2);
            EngineerNumber number3 = new EngineerNumber(233e-44, "N", false, 2);
            EngineerNumber number4 = new EngineerNumber(2.33e9, "T", false, 2);
            EngineerNumber number5 = new EngineerNumber(2.33e44, "cad", false, 2);
            EngineerNumber number6 = new EngineerNumber(233e3, "", false, 2);

            string output1 = number1.ToString();
            string output2 = number2.ToString();
            string output3 = number3.ToString();
            string output4 = number4.ToString();
            string output5 = number5.ToString();
            string output6 = number6.ToString();

            Assert.AreEqual("2.3nA", output1);
            Assert.AreEqual("23e-45L", output2);
            Assert.AreEqual("2.3e-42N", output3);
            Assert.AreEqual("2.3GT", output4);
            Assert.AreEqual("230e42cad", output5);
            Assert.AreEqual("230e3", output6); // No SI prefix when no unit
        }

        [TestMethod()]
        public void DecimalToEngineerTest_noUnit()
        {
            EngineerNumber number1 = new EngineerNumber(2.33e-9);
            EngineerNumber number2 = new EngineerNumber(2.33e-44);
            EngineerNumber number3 = new EngineerNumber(233e-44);
            EngineerNumber number4 = new EngineerNumber(2.33e9);
            EngineerNumber number5 = new EngineerNumber(2.33e44);
            EngineerNumber number6 = new EngineerNumber(233e44);

            string output1 = number1.ToString();
            string output2 = number2.ToString();
            string output3 = number3.ToString();
            string output4 = number4.ToString();
            string output5 = number5.ToString();
            string output6 = number6.ToString();

            Assert.AreEqual("2.33e-9", output1);
            Assert.AreEqual("23.3e-45", output2);
            Assert.AreEqual("2.33e-42", output3);
            Assert.AreEqual("2.33e9", output4);
            Assert.AreEqual("233e42", output5);
            Assert.AreEqual("23.3e45", output6);
        }

        [TestMethod()]
        public void DecimalToEngineerTest_Bounds()
        {
            EngineerNumber number1 = new EngineerNumber(double.NaN, "", false, 1);
            EngineerNumber number2 = new EngineerNumber(double.NegativeInfinity, "", false, 2);
            EngineerNumber number3 = new EngineerNumber(double.PositiveInfinity, "", false, 3);
            EngineerNumber number4 = new EngineerNumber(double.MaxValue, "", false, 4);
            EngineerNumber number5 = new EngineerNumber(double.MinValue, "", false, 5);
            EngineerNumber number6 = new EngineerNumber(0, "", false, 6);

            string output1 = number1.ToString();
            string output2 = number2.ToString();
            string output3 = number3.ToString();
            string output4 = number4.ToString();
            string output5 = number5.ToString();
            string output6 = number6.ToString();

            Assert.AreEqual("NaN", output1);
            Assert.AreEqual("-∞", output2);
            Assert.AreEqual("∞", output3);
            Assert.AreEqual("179.8e306", output4);
            Assert.AreEqual("-179.77e306", output5);
            Assert.AreEqual("0", output6);
        }

        [TestMethod()]
        public void EngineerToDecimalTest_basic_units()
        {
            string input1 = "2.33e-9 k";
            string input2 = "23.3e-45 kg";
            string input3 = "2.33e-42 lux";
            string input4 = "2.33e9 lol";
            string input5 = "233e42mm";
            string input6 = "23.3e45kg";

            double output1 = EngineerNumber.EngineerToDecimal(input1);
            double output2 = EngineerNumber.EngineerToDecimal(input2);
            double output3 = EngineerNumber.EngineerToDecimal(input3);
            double output4 = EngineerNumber.EngineerToDecimal(input4);
            double output5 = EngineerNumber.EngineerToDecimal(input5);
            double output6 = EngineerNumber.EngineerToDecimal(input6);

            Assert.AreEqual(2.33e-9, output1);
            Assert.AreEqual(2.33e-44, output2);
            Assert.AreEqual(233e-44, output3);
            Assert.AreEqual(2.33e9, output4);
            Assert.AreEqual(2.33e44, output5);
            Assert.AreEqual(233e44, output6);
        }

        [TestMethod()]
        public void EngineerToDecimalTest_SI()
        {
            string input1 = "2.33n";
            string input2 = "23.3 kg";
            string input3 = "2.33 mT";
            string input4 = "2.33 Glux";
            string input5 = "233 ";
            string input6 = " 23.3 mm";

            double output1 = EngineerNumber.EngineerToDecimal(input1);
            double output2 = EngineerNumber.EngineerToDecimal(input2);
            double output3 = EngineerNumber.EngineerToDecimal(input3);
            double output4 = EngineerNumber.EngineerToDecimal(input4);
            double output5 = EngineerNumber.EngineerToDecimal(input5);
            double output6 = EngineerNumber.EngineerToDecimal(input6);

            Assert.AreEqual(2.33e-9, output1);
            Assert.AreEqual(23.3e3, output2);
            Assert.AreEqual(2.33e-3, output3);
            Assert.AreEqual(2.33e9, output4);
            Assert.AreEqual(233, output5);
            Assert.AreEqual(23.3e-3, output6);
        }

        [TestMethod()]
        public void EngineerToDecimalTest_Fails()
        {
            string input1 = "2.33+n";
            string input2 = "23.3  kg";
            string input3 = "2.33 m m";
            string input4 = "2.33eklux";
            string input5 = "233 .";
            string input6 = "a23.3 mm";

            double output1 = EngineerNumber.EngineerToDecimal(input1);
            double output2 = EngineerNumber.EngineerToDecimal(input2);
            double output3 = EngineerNumber.EngineerToDecimal(input3);
            double output4 = EngineerNumber.EngineerToDecimal(input4);
            double output5 = EngineerNumber.EngineerToDecimal(input5);
            double output6 = EngineerNumber.EngineerToDecimal(input6);

            Assert.AreEqual(double.NaN, output1);
            Assert.AreEqual(double.NaN, output2);
            Assert.AreEqual(double.NaN, output3);
            Assert.AreEqual(double.NaN, output4);
            Assert.AreEqual(double.NaN, output5);
            Assert.AreEqual(double.NaN, output6);
        }

        [TestMethod()]
        public void EngineerNumberTest_Precisions()
        {
            EngineerNumber number1 = new EngineerNumber(23333.33, "", false, 1);
            EngineerNumber number2 = new EngineerNumber(23333.33, "", false, 2);
            EngineerNumber number3 = new EngineerNumber(23333.33, "", false, 3);
            EngineerNumber number4 = new EngineerNumber(23333.33, "", false, 4);
            EngineerNumber number5 = new EngineerNumber(23333.33, "", false, 5);
            EngineerNumber number6 = new EngineerNumber(23333.33, "", false, 6);

            string output1 = number1.ToString();
            string output2 = number2.ToString();
            string output3 = number3.ToString();
            string output4 = number4.ToString();
            string output5 = number5.ToString();
            string output6 = number6.ToString();

            Assert.AreEqual("20e3", output1);
            Assert.AreEqual("23e3", output2);
            Assert.AreEqual("23.3e3", output3);
            Assert.AreEqual("23.33e3", output4);
            Assert.AreEqual("23.333e3", output5);
            Assert.AreEqual("23.3333e3", output6);
        }

        [TestMethod()]
        public void EngineerNumberTest_Precisions_TrailingZeros()
        {
            EngineerNumber number1 = new EngineerNumber(23333.33, "", false, 1);
            EngineerNumber number2 = new EngineerNumber(23333.33, "", false, 2);
            EngineerNumber number3 = new EngineerNumber(23333.33, "", false, 3);
            EngineerNumber number4 = new EngineerNumber(23333.33, "", false, 4);
            EngineerNumber number5 = new EngineerNumber(23333.33, "", false, 5);
            EngineerNumber number6 = new EngineerNumber(23333.33, "", false, 6);

            string output1 = number1.ToString();
            string output2 = number2.ToString();
            string output3 = number3.ToString();
            string output4 = number4.ToString();
            string output5 = number5.ToString();
            string output6 = number6.ToString();

            Assert.AreEqual("20e3", output1);
            Assert.AreEqual("23e3", output2);
            Assert.AreEqual("23.3e3", output3);
            Assert.AreEqual("23.33e3", output4);
            Assert.AreEqual("23.333e3", output5);
            Assert.AreEqual("23.3333e3", output6);
        }
    }
}
