using System;
using System.Windows.Forms;

namespace ESNLib.Examples
{
    public partial class ex_textbox_watermark : Form
    {
        public ex_textbox_watermark()
        {
            InitializeComponent();

            textboxWatermark3.Text = richTextboxWatermark1.WatermarkText = textboxWatermark1.WatermarkText;
            textBox1.BackColor = richTextboxWatermark1.WatermarkColor = textboxWatermark1.WatermarkColor;
            textBox2.Text = textboxWatermark1.WatermarkColor.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textboxWatermark1.WatermarkColor = richTextboxWatermark1.WatermarkColor = textBox1.BackColor = colorDialog1.Color;
                textBox2.Text = colorDialog1.Color.ToString();
            }
        }

        private void textboxWatermark3_TextChanged(object sender, EventArgs e)
        {
            textboxWatermark1.WatermarkText = richTextboxWatermark1.WatermarkText = textboxWatermark3.Text;
        }
    }
}
