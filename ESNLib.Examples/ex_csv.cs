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
            CsvImportAs<MyClass> csvi = new CsvImportAs<MyClass>();

            string[] headers = new string[]
            {
                "C1","C2","C3","C4","C5","C6"
            };

            csvi.AskUserHeadersLinks(new MyClass[] { c1, c2 }, headers);
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
