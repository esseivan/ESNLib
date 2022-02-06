using EsseivaN.Tools;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_settings_manager : Form
    {
        List<setting> settingsList = new List<setting>();
        Dictionary<int, string> settingsNames = new Dictionary<int, string>();

        EsseivaN.Tools.SettingsManager<setting> settingsManager;

        // The class to save
        class setting
        {
            public int index;
            public string data1;
            public string data2;

            public setting(int index)
            {
                this.index = index;
            }
        }

        // Function to show name
        private string getName(setting settings)
        {
            return settingsNames?[settings.index];
        }

        public ex_settings_manager()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // Create new file
            settingsManager = new SettingsManager<setting>(getName);

            btnGetAll.PerformClick();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                settingsManager = new SettingsManager<setting>(getName);
                var t = settingsManager.Load(openFileDialog1.FileName);

                settingsNames.Clear();
                foreach (var item in t)
                {
                    settingsNames.Add(item.Value.index, item.Key);
                }

                btnGetAll.PerformClick();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (settingsManager == null)
                {
                    return;
                }

                settingsManager.Save(saveFileDialog1.FileName);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIndex.Text, out int index))
            {
                settingsManager.RemoveSetting(settingsNames[index]);
                btnGetAll.PerformClick();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (settingsManager == null)
            {
                return;
            }

            if (int.TryParse(txtIndex.Text, out int index))
            {
                settingsNames[index] = txtName.Text;
                settingsManager.AddSetting(new setting(index) { data1 = txtData1.Text, data2 = txtData2.Text });
                btnGetAll.PerformClick();
            }
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            settingsManager.AddSettingRange(richTextBox1.Text);
            btnGetAll.PerformClick();
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = settingsManager.GenerateFileData();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (settingsManager == null)
            {
                return;
            }

            if (int.TryParse(txtIndex.Text, out int index))
            {
                txtName.Text = txtData1.Text = txtData2.Text = string.Empty;

                if (settingsNames.ContainsKey(index))
                {
                    setting setting = settingsManager.GetSetting(settingsNames[index]);
                    if (setting == null)
                    {
                        txtName.Text = txtData1.Text = txtData2.Text = string.Empty;
                    }
                    else
                    {
                        txtName.Text = getName(setting);
                        txtData1.Text = setting.data1.ToString();
                        txtData2.Text = setting.data2.ToString();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIndex.Text, out int index))
            {
                setting setting = settingsManager.GetSetting(settingsNames[index]);
                if (setting == null)
                {
                    setting = new setting(index);
                }

                setting.data1 = txtData1.Text;
                setting.data2 = txtData2.Text;
                settingsNames[index] = txtName.Text;
                btnGetAll.PerformClick();
            }
        }
    }
}
