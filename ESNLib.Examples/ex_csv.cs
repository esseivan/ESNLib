using ESNLib.Tools.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESNLib.Examples
{
    public partial class ex_csv : Form
    {
        public ex_csv()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Generate the CSV data
        private string GenerateCSV(IEnumerable<MyClass> classes, bool generateHeader = true)
        {
            string csvOutput = generateHeader ? $"dummy2,x,n,y,dummy2,Text\n" : string.Empty;

            foreach (MyClass item in classes)
            {
                csvOutput += $"DummyText,{item.x},{item.n},{item.y},DummyText2,{item.text}\n";
            }

            return csvOutput;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            // generate string
            string s = GenerateCSV(new MyClass[] { c1, c2 });

            CsvImportAs<MyClass> csvi = new CsvImportAs<MyClass>();

            csvi.AskUserHeadersLinks(s);
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
    }
}
