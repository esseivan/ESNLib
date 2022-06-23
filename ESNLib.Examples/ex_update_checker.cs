using ESNLib.Tools;
using System;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_update_checker : Form
    {
        public ex_update_checker()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Version.TryParse(textboxWatermark1.Text, out Version v))
            {
                MessageBox.Show("Invalid version !!");
                return;
            }

            UpdateChecker_Runner.CheckUpdateAndProcess(textboxWatermark2.Text, textboxWatermark1.Text, appExit, showMsg);
        }

        internal void showMsg(string msg, string title, int icon)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, (MessageBoxIcon)icon);
        }

        internal void appExit()
        {
            Application.Exit();
        }

        private void ex_update_checker_Load(object sender, EventArgs e)
        {
            textboxWatermark1.Text = "1.0";
            textboxWatermark2.Text = @"http://www.ESNLib.ch/files/softwares/resistortool/version.xml";
        }
    }
}
