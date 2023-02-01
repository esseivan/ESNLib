using ESNLib.Tools;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ESNLib.Examples
{
    public partial class ex_setting_manager : Form
    {
        List<Setting> settings = new List<Setting>();

        // The class to save
        private class Setting
        {
            public string data1 { get; set; } = string.Empty;
            public string data2 = string.Empty;

            public override string ToString()
            {
                return "(" + data1?.ToString() + "&" + data2?.ToString() + ")";
            }
        }

        public ex_setting_manager()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            settings = new List<Setting>
            {
                new Setting() { data1 = "hello", data2 = "world" },
                new Setting() { data1 = DateTime.Now.ToString(), data2 = "bar" },
            };
            Console.WriteLine(string.Join(",", settings));
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SettingsManager.LoadFrom(openFileDialog1.FileName, out settings);
                Console.WriteLine(string.Join(",", settings));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SettingsManager.SaveTo(saveFileDialog1.FileName, settings);
            }
        }
    }
}
