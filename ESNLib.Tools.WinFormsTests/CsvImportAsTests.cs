using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESNLib.Tools.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ESNLib.Tools.WinForms.Tests
{
    [TestClass()]
    public class CsvImportAsTests
    {
        private string GenerateCSV(MyClass item, bool generateHeader = true)
        {
            string csvOutput = generateHeader ? $"dummy1,x,n,y,dummy2,text\n" : string.Empty;
            csvOutput += $"DummyText,{item.x},{item.n},{item.y},DummyText2,{item.text}\n";

            return csvOutput;
        }

        private string GenerateCSV(IEnumerable<MyClass> classes, bool generateHeader = true)
        {
            string csvOutput = generateHeader ? $"dummy1,x,n,y,dummy2,text\n" : string.Empty;

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
            public int y; // This one is invisible...

            public float n { get; set; }

            public string text { get; set; }
        }

        [TestMethod()]
        public void TestImport_Single()
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
            Assert.AreEqual(0, result[0].y);
            Assert.AreEqual(3.0, result[0].n);
            Assert.AreEqual("name", result[0].text);
        }

        [TestMethod()]
        public void TestImport_Multiple()
        {
            // CSV data
            MyClass c1 = new MyClass()
            {
                x = 1,
                y = 2,
                n = 3.0f,
                text = "name",
            };
            MyClass c2 = new MyClass()
            {
                x = 4,
                y = 5,
                n = 6.66f,
                text = "newname",
            };
            string s = GenerateCSV(new MyClass[] { c1, c2 });

            CsvImportAs<MyClass> csvi = new CsvImportAs<MyClass>();
            List<MyClass> result = csvi.ImportData(s);

            Assert.AreEqual(2, result.Count);
            MyClass r = result[0];
            MyClass comp = c1;
            Assert.AreEqual(comp.x, r.x);
            Assert.AreEqual(0, r.y);
            Assert.AreEqual(comp.n, r.n);
            Assert.AreEqual(comp.text, r.text);
            r = result[1];
            comp = c2;
            Assert.AreEqual(comp.x, r.x);
            Assert.AreEqual(0, r.y);
            Assert.AreEqual(comp.n, r.n);
            Assert.AreEqual(comp.text, r.text);
        }

        [TestMethod()]
        public void TestImport_CustomHeaders()
        {
            // CSV data
            MyClass c1 = new MyClass()
            {
                x = 1,
                y = 2,
                n = 3.0f,
                text = "name",
            };
            MyClass c2 = new MyClass()
            {
                x = 4,
                y = 5,
                n = 6.66f,
                text = "newname",
            };
            string s = GenerateCSV(new MyClass[] { c1, c2 }, false);
            s = $"C1,C2,C3,C4,C5,C6\n{s}"; //Custom header

            CsvImportAs<MyClass> csvi = new CsvImportAs<MyClass>();

            // Define headers links
            Dictionary<string, string> links = new Dictionary<string, string>()
            {
                { "C2", "x" },
                { "C3", "n" },
                { "C4", "y" },
                { "C6", "text" },
            };
            List<MyClass> result = csvi.ImportData(s, links);

            Assert.AreEqual(2, result.Count);
            MyClass r = result[0];
            MyClass comp = c1;
            Assert.AreEqual(comp.x, r.x);
            Assert.AreEqual(0, r.y);
            Assert.AreEqual(comp.n, r.n);
            Assert.AreEqual(comp.text, r.text);
            r = result[1];
            comp = c2;
            Assert.AreEqual(comp.x, r.x);
            Assert.AreEqual(0, r.y);
            Assert.AreEqual(comp.n, r.n);
            Assert.AreEqual(comp.text, r.text);
        }

        [TestMethod()]
        public void TestImport_MissingData()
        {
            // CSV data
            MyClass c1 = new MyClass()
            {
                x = 1,
                y = 2,
                n = 3.0f,
                text = "name",
            };
            MyClass c2 = new MyClass()
            {
                x = 4,
                y = 5,
                n = 6.66f,
                text = "newname",
            };
            string s = GenerateCSV(new MyClass[] { c1, c2 }, false);
            s = $"C1,C2,C3,C4,C5,C6\n{s}"; //Custom header
            // Add custom line
            s += $"d,666,,,,\n";

            CsvImportAs<MyClass> csvi = new CsvImportAs<MyClass>();

            // Define headers links
            Dictionary<string, string> links = new Dictionary<string, string>()
            {
                { "C2", "x" },
                { "C3", "n" },
                { "C4", "y" },
                { "C6", "text" },
            };
            List<MyClass> result = csvi.ImportData(s, links);

            Assert.AreEqual(3, result.Count);
            MyClass r = result[0];
            MyClass comp = c1;
            Assert.AreEqual(comp.x, r.x);
            Assert.AreEqual(0, r.y);
            Assert.AreEqual(comp.n, r.n);
            Assert.AreEqual(comp.text, r.text);
            r = result[1];
            comp = c2;
            Assert.AreEqual(comp.x, r.x);
            Assert.AreEqual(0, r.y);
            Assert.AreEqual(comp.n, r.n);
            Assert.AreEqual(comp.text, r.text);
            r = result[2];

            Assert.AreEqual(666, r.x);
            Assert.AreEqual(0, r.y);
            Assert.AreEqual(0, r.n);
            Assert.AreEqual(default(String), r.text);
        }

        [TestMethod()]
        public void AskUserHeadersLinksTest_Cancel()
        {
            return;

            // CSV data
            MyClass c1 = new MyClass()
            {
                x = 1,
                y = 2,
                n = 3.0f,
                text = "name",
            };
            MyClass c2 = new MyClass()
            {
                x = 4,
                y = 5,
                n = 6.66f,
                text = "newname",
            };
            string s = GenerateCSV(new MyClass[] { c1, c2 });

            CsvImportAs<MyClass> csvi = new CsvImportAs<MyClass>();

            var result = csvi.AskUserHeadersLinks(s);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void AskUserHeadersLinksTest_Accept_AsIs()
        {
            return;

            // CSV data
            MyClass c1 = new MyClass()
            {
                x = 1,
                y = 2,
                n = 3.0f,
                text = "name",
            };
            MyClass c2 = new MyClass()
            {
                x = 4,
                y = 5,
                n = 6.66f,
                text = "newname",
            };
            string s = GenerateCSV(new MyClass[] { c1, c2 });

            CsvImportAs<MyClass> csvi = new CsvImportAs<MyClass>();

            var result = csvi.AskUserHeadersLinks(s);

            Assert.IsNotNull(result);
            Assert.AreEqual("x", result["x"].Name);
            Assert.AreEqual("n", result["n"].Name);
            Assert.AreEqual("text", result["text"].Name);
        }
    }
}
