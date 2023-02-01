using ESNLib.Tools;
using ESNLib.Tools.WinForms;
using System;
using System.Windows.Forms;

namespace ESNLib.Examples
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

            UpdateChecker uc = new UpdateChecker(textboxWatermark2.Text, textboxWatermark1.Text);
            if (uc.CheckUpdates())
            {
                showMsg(
                    "New update : "
                        + uc.Result.LastVersion
                        + "\nDownload at : "
                        + uc.Result.UpdateURL,
                    "Update !",
                    MessageBoxIcon.None
                );
            }
        }

        internal void showMsg(string msg, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, icon);
        }

        internal void appExit()
        {
            Application.Exit();
        }

        private void ex_update_checker_Load(object sender, EventArgs e)
        {
            textboxWatermark1.Text = "1.0";
            textboxWatermark2.Text =
                @"http://www.ESNLib.ch/files/softwares/resistortool/version.xml";
        }
    }
}
