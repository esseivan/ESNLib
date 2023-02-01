using ESNLib.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ESNLib.Tools.UnitTests
{
    [TestClass]
    public class FlagsTests
    {
        [TestMethod]
        public void FlagsSetBits_0To32_1FlagAndData()
        {
            // Arrange
            Flags flags_Infinite = new Flags();
            int writeData = 0x55A5A5A5;

            // Act
            flags_Infinite.SetBits(0, 32, writeData);
            int readData = flags_Infinite.FlagList[0];

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 1);
            Assert.AreEqual(readData, (int)writeData);
        }

        [TestMethod]
        public void FlagsSetBits_16To32_2FlagAndData()
        {
            // Arrange
            Flags flags_Infinite = new Flags();
            int writeData = 0x55A5A5A5;

            // Act
            flags_Infinite.SetBits(16, 32, writeData);
            int readData = flags_Infinite.GetBits(16, 32);

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 2);
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void FlagsSetBits_28To32_2FlagAndData()
        {
            // Arrange
            Flags flags_Infinite = new Flags();
            int writeData = 0x55A5A5A5;

            // Act
            flags_Infinite.SetBits(28, 32, writeData);
            int readData = flags_Infinite.GetBits(28, 32);

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 2);
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void FlagsSetBits_68To32_4FlagAndData()
        {
            // Arrange
            Flags flags_Infinite = new Flags();
            int writeData = 0x55A5A5A5;

            // Act
            flags_Infinite.SetBits(68, 32, writeData);
            int readData = flags_Infinite.GetBits(68, 32);

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 4);
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void FlagsGetBits_0To32_1FlagAndData()
        {
            // Arrange
            Flags flags_Infinite = new Flags();
            int writeData = 0x55A5A5A5;

            // Act
            flags_Infinite.SetBits(0, 32, writeData);
            int readData = flags_Infinite.GetBits(0, 32);

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 1);
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void FlagsGetBits_16To32_2FlagAndData()
        {
            // Arrange
            Flags flags_Infinite = new Flags();
            int writeData = 0x55A5A5A5;

            // Act
            flags_Infinite.SetBits(16, 32, writeData);
            int readData = flags_Infinite.GetBits(16, 32);

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 2);
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void FlagsGetBits_28To32_2FlagAndData()
        {
            // Arrange
            Flags flags_Infinite = new Flags();
            int writeData = 0x55A5A5A5;

            // Act
            flags_Infinite.SetBits(28, 32, writeData);
            int readData = flags_Infinite.GetBits(28, 32);

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 2);
            Assert.AreEqual(readData, writeData);
        }

        [TestMethod]
        public void FlagsSetBits_MultipleWrites_DataComplete()
        {
            // wanted pattern :
            // 0x12312345
            // 0x67812123
            // Writes : 0, 3, 0x123
            //          3, 2, 0x12
            //          5, 8, 0x12345678
            //          13, 3, 0x123
            // Arrange
            Flags flags_Infinite = new Flags();
            int c1,
                c2,
                r11,
                r12,
                r21,
                r22;

            // Act
            // ##### First order
            // Initialize content
            flags_Infinite.SetBits(0, 3 * 4, 0x123);
            flags_Infinite.SetBits(3 * 4, 2 * 4, 0x12);
            flags_Infinite.SetBits(5 * 4, 8 * 4, 0x12345678);
            flags_Infinite.SetBits(13 * 4, 3 * 4, 0x123);
            c1 = flags_Infinite.FlagList.Count;
            r11 = flags_Infinite.GetBits(0, 32);
            r12 = flags_Infinite.GetBits(32, 32);
            // ##### Second order
            // Initialize content
            flags_Infinite.SetBits(13 * 4, 3 * 4, 0x123);
            flags_Infinite.SetBits(0, 3 * 4, 0x123);
            flags_Infinite.SetBits(5 * 4, 8 * 4, 0x12345678);
            flags_Infinite.SetBits(3 * 4, 2 * 4, 0x12);
            c2 = flags_Infinite.FlagList.Count;
            r21 = flags_Infinite.GetBits(0, 32);
            r22 = flags_Infinite.GetBits(32, 32);

            // Act
            Assert.IsTrue(c1 == 2);
            Assert.IsTrue(c2 == 2);

            Assert.AreEqual(r11, 0x67812123);
            Assert.AreEqual(r12, 0x12312345);

            Assert.AreEqual(r21, 0x67812123);
            Assert.AreEqual(r22, 0x12312345);
        }

        [TestMethod]
        public void FlagsSetBits_Mask_DataComplete()
        {
            // wanted pattern :
            // 0x00654300
            // Arrange
            Flags flags_Infinite = new Flags();

            // Act
            flags_Infinite.SetBits(0, 6 * 4, 0x87654321);
            flags_Infinite.SetBits(0, 2 * 4, 0);
            int readData = flags_Infinite.GetBits(0, 32);

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 1);
            Assert.AreEqual(readData, 0x00654300);
        }

        [TestMethod]
        public void FlagsSetBits_FullTest_GetEqualsSet()
        {
            // Arrange
            Flags flags_Infinite = new Flags();
            int[] Pattern_index = { 0, 40, 80, 120, 160, 200, 235, 270, 305, 340 };
            Random rnd = new Random();
            int[] writeData = new int[10];

            // Act
            for (int i = 0; i < 10; i++)
            {
                writeData[i] = (int)(rnd.NextDouble() * int.MaxValue);
                flags_Infinite.SetBits(Pattern_index[i], 32, writeData[i]);
            }
            int[] readData = new int[10];
            for (int i = 0; i < 10; i++)
            {
                readData[i] = flags_Infinite.GetBits(Pattern_index[i], 32);
            }

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 12);
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(readData[i], writeData[i]);
            }
        }

        [TestMethod]
        public void FlagsDisplayBinary_Normal_DataValid()
        {
            // Arrange
            Flags flags_Infinite = new Flags();
            int writeData = 0b0110011000111;

            // Act
            flags_Infinite.SetBits(28, 11, writeData);
            string binary = flags_Infinite.DisplayBinary(28, 11);

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 2);
            Assert.AreEqual(binary, "10011000111");
        }

        [TestMethod]
        public void FlagsDisplayHex_Normal_DataValid()
        {
            // Arrange
            Flags flags_Infinite = new Flags();
            int writeData = 0x8563224;

            // Act
            flags_Infinite.SetBits(28, 5 * 4, writeData);
            string hex = flags_Infinite.DisplayHex(28, 5 * 4);

            // Assert
            Assert.IsTrue(flags_Infinite.FlagList.Count == 2);
            Assert.AreEqual(hex, "63224");
        }
    }
}
