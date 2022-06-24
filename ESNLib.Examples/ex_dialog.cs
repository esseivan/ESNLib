using ESNLib.Controls;
using ESNLib.Tools;
using System;
using System.Windows.Forms;

namespace ESNLib.Examples
{
    public partial class ex_dialog : Form
    {
        public ex_dialog()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t = ((Dialog.ButtonType[])Enum.GetValues(typeof(Dialog.ButtonType)));
            var t2 = ((Dialog.DialogIcon[])Enum.GetValues(typeof(Dialog.DialogIcon)));
            Dialog.DialogConfig dialogConfig = new Dialog.DialogConfig()
            {
                CustomButton1Text = mB1.Text,
                CustomButton2Text = mB2.Text,
                CustomButton3Text = mB3.Text,
                Message = mMsg.Text,
                Title = mTitle.Text,
                Input = false,
                Button1 = t[mL1.SelectedIndex],
                Button2 = t[mL2.SelectedIndex],
                Button3 = t[mL3.SelectedIndex],
                Icon = t2[mI1.SelectedIndex],
            };

            label1.Text = string.Empty;
            label1.Text = Dialog.ShowDialog(dialogConfig).DialogResult.ToString();
        }

        private void ex_dialogInput_Load(object sender, EventArgs e)
        {
            mL1.Items.Clear();
            mL2.Items.Clear();
            mL3.Items.Clear();

            mL1.Items.AddRange(Enum.GetNames(typeof(Dialog.ButtonType)));
            mL2.Items.AddRange(Enum.GetNames(typeof(Dialog.ButtonType)));
            mL3.Items.AddRange(Enum.GetNames(typeof(Dialog.ButtonType)));
            mI1.Items.AddRange(Enum.GetNames(typeof(Dialog.DialogIcon)));

            mL1.SelectedIndex = mL2.SelectedIndex = mL3.SelectedIndex = mI1.SelectedIndex = 0;

            mL1.SelectedIndex = 1;
            mL2.SelectedIndex = 8;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(mMsg.Text, mTitle.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
    }
}