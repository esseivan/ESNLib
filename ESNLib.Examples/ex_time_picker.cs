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
    public partial class ex_time_picker : Form
    {
        public ex_time_picker()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timePicker_21.Populate(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timePicker_21.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timePicker_21.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Heures
            timePicker_21.Populate(12, 80);
            timePicker_21.Populate(12, 105);

            for (int i = 0; i < 24; i++)
            {
                timePicker_21.SetTextNext(i.ToString("00"));
            }

            // Minutes
            //timePicker_21.Populate(12, 105);
        }
    }
}
