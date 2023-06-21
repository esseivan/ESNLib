using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESNLib.Tools.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools.WinForms.Tests
{
    [TestClass()]
    public class CsvImportAsTests
    {
        private string GenerateCSV(MyClass item)
        {
            string csvOutput = $"dummy2,x,n,y,dummy2,text\n";
            csvOutput += $"DummyText,{item.x},{item.n},{item.y},DummyText2,{item.text}\n";

            return csvOutput;
        }

        private string GenerateCSV(IEnumerable<MyClass> classes)
        {
            string csvOutput = $"dummy2,x,n,y,dummy2,text\n";

            foreach (MyClass item in classes)
            {
                csvOutput += $"DummyText,{item.x},{item.n},{item.y},DummyText2,{item.text}\n";
            }

            return csvOutput;
        }

        /// <summary>
        /// Template class for testing purposes
        /// </summary>
        private class MyClass
        {
            public int x { get; set; }
            public int y;

            public float n;

            public string text;
        }


        [TestMethod()]
        public void TestImport()
        {
            // CSV data
            MyClass c = new MyClass()
            {
                x = 1,
                y = 2,
                n = 3.0f,
                text = "name",
            };
            string s = GenerateCSV(c);

            CsvImportAs<MyClass> csvi = new CsvImportAs<MyClass>();
            List<MyClass> result = csvi.ImportData(s);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].x);
            Assert.AreEqual(2, result[0].y);
            Assert.AreEqual(3.0, result[0].n);
            Assert.AreEqual("name", result[0].text);
        }
    }
}