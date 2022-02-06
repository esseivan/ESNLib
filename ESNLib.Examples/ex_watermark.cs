using EsseivaN.Tools;
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
    public partial class ex_watermark : Form
    {
        public ex_watermark()
        {
            InitializeComponent();
        }

        Watermark watermark_combobox;
        Watermark watermark_numeric;
        Watermark watermark_textbox;
        private void ex_watermark_Load(object sender, EventArgs e)
        {
            watermark_combobox = new Watermark();
            watermark_numeric = new Watermark();
            watermark_textbox = new Watermark();

            watermark_textbox.getText = getText_textbox;
            watermark_textbox.setText = setText_textbox;
            watermark_textbox.setFontColor = setFontColor_textbox;
            watermark_textbox.Enable();


            watermark_numeric.EmptyText = "0";
            watermark_numeric.WatermarkText = "100";
            watermark_numeric.getText = getText_numeric;
            watermark_numeric.setText = setText_numeric;
            watermark_numeric.setFontColor = setFontColor_numeric;
            watermark_numeric.Enable();


            watermark_combobox.getText = getText_combobox;
            watermark_combobox.setText = setText_combobox;
            watermark_combobox.setFontColor = setFontColor_combobox;
            watermark_combobox.Enable();
        }

        // Textbox

        private string getText_textbox() => textBox1.Text;

        private void setText_textbox(string text) => textBox1.Text = text;

        private void setFontColor_textbox(Color color) => textBox1.ForeColor = color;

        private void textBox1_Enter(object sender, EventArgs e)
        {
            watermark_textbox.onFocusEnter();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            watermark_textbox.onFocusLost();
        }

        // Numeric up down

        private string getText_numeric() => numericUpDown1.Value.ToString();

        private void setText_numeric(string text) => numericUpDown1.Value = int.Parse(text);

        private void setFontColor_numeric(Color color) => numericUpDown1.ForeColor = color;

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            watermark_numeric.onFocusEnter();
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            watermark_numeric.onFocusLost();
        }

        // ComboBox

        private string getText_combobox() => comboBox1.Text;

        private void setText_combobox(string text) => comboBox1.Text = text;

        private void setFontColor_combobox(Color color) => comboBox1.ForeColor = color;

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            watermark_combobox.onFocusEnter();
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            watermark_combobox.onFocusLost();
        }
    }
}
