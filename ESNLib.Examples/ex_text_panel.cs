using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_text_panel : Form
    {
        public ex_text_panel()
        {
            InitializeComponent();
            textboxWatermark1.Text = "Copy to clipboard";
        }

        private void textPanel1_Clipboard_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Load button clicked !";
            textboxWatermark2.Focus();
        }

        private void textPanel1_Clipboard_Delete(object sender, EventArgs e)
        {
            textBox1.Text = "Delete button clicked !";
        }

        private void textPanel1_Clipboard_Show(object sender, EventArgs e)
        {
            textBox1.Text = "Show button clicked !";
        }

        private void textboxWatermark1_TextChanged(object sender, EventArgs e)
        {
            textPanel1.Text = textboxWatermark1.Text;
        }
    }
}
