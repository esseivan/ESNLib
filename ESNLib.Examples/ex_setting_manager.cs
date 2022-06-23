using ESNLib.Tools;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_setting_manager : Form
    {
        Setting setting = new Setting();
        SettingManager<Setting> settingManager;

        // The class to save
        class Setting
        {
            public string data1;
            public string data2;
        }

        public ex_setting_manager()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // Create new file
            settingManager = new SettingManager<Setting>();

            btnGetAll.PerformClick();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                settingManager = new SettingManager<Setting>();
                settingManager.Load(openFileDialog1.FileName, out setting);

                btnGetAll.PerformClick();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (settingManager == null)
                {
                    return;
                }

                settingManager.Save(saveFileDialog1.FileName);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            settingManager.Clear();
            btnGetAll.PerformClick();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (settingManager == null)
            {
                return;
            }

            settingManager.SetSetting(new Setting() { data1 = txtData1.Text, data2 = txtData2.Text });
            btnGetAll.PerformClick();
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = settingManager.GenerateFileData();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (settingManager == null)
            {
                return;
            }

            txtData1.Text = txtData2.Text = string.Empty;

            Setting setting = settingManager.GetSetting();
            if (setting == null)
            {
                txtData1.Text = txtData2.Text = string.Empty;
            }
            else
            {
                txtData1.Text = setting.data1.ToString();
                txtData2.Text = setting.data2.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Setting setting = settingManager.GetSetting();
            if (setting == null)
            {
                setting = new Setting();
            }

            setting.data1 = txtData1.Text;
            setting.data2 = txtData2.Text;
            btnGetAll.PerformClick();
        }
    }
}
