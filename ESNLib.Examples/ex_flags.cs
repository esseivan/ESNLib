using ESNLib.Tools;
using System;
using System.Windows.Forms;

namespace ESNLib.Examples
{
    public partial class ex_flags : Form
    {
        Flags flags = new Flags();
        public ex_flags()
        {
            InitializeComponent();
            numericUpDown3.Maximum = ulong.MaxValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var r = flags.GetBits((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            if (r == -1)
            {
                MessageBox.Show("Error, no data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                numericUpDown3.Value = r;
                displayBox();
            }
        }

        private void ex_flags_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            flags.SetBits((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            displayBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var r = flags.GetBits((int)numericUpDown1.Value, 1);
            if (r == -1)
            {
                MessageBox.Show("Error, no data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                numericUpDown3.Value = r;
                displayBox();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var r = flags.GetBit((int)numericUpDown1.Value) ? 1 : 0;
            if (r == -1)
            {
                MessageBox.Show("Error, no data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                numericUpDown3.Value = r;
                displayBox();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            flags.SetBits((int)numericUpDown1.Value, (int)numericUpDown3.Value);
            displayBox();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            flags.SetBit((int)numericUpDown1.Value, numericUpDown3.Value > 0);
            displayBox();
        }

        private void displayBox()
        {
            textBox1.Text = flags.DisplayBinary(0);
            textBox2.Text = flags.DisplayHex(0);
        }
    }
}
