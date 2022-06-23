using ESNLib.Tools;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_logger : Form
    {
        Logger logger = new Logger();

        string path;

        public ex_logger()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteLog(richTextboxWatermark1.Text);
        }

        private void WriteLog(string data, Logger.Log_level level)
        {
            if (logger == null)
            {
                MessageBox.Show("Enable logger before writing in log !");
                return;
            }

            if (comboBox4.SelectedIndex == comboBox4.Items.Count - 1)
            {
                logger.WriteLog(data, textboxWatermark2.Text);
            }
            else
            {
                logger.WriteLog(data, level);
            }
        }

        private void WriteLog(string data)
        {
            WriteLog(data, (Logger.Log_level)comboBox4.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                Process.Start(path);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            logger.LogToFile_CustomSuffix = textboxWatermark1.Text;
            logger.LogToFile_FilePath = path;
            logger.LogToFile_SaveMode = (Logger.SaveFileMode)comboBox1.SelectedIndex;
            logger.LogToFile_SuffixMode = (Logger.Suffix_mode)comboBox2.SelectedIndex;
            logger.LogToFile_WriteMode = (Logger.WriteMode)comboBox3.SelectedIndex;

            logger.Enable();
        }

        private void ex_logger_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Enum.GetNames(typeof(Logger.SaveFileMode)));
            comboBox2.Items.AddRange(Enum.GetNames(typeof(Logger.Suffix_mode)));
            comboBox3.Items.AddRange(Enum.GetNames(typeof(Logger.WriteMode)));
            comboBox4.Items.AddRange(Enum.GetNames(typeof(Logger.Log_level)));
            comboBox4.Items.Add("Custom");
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = comboBox3.SelectedIndex = comboBox4.SelectedIndex = 0;
        }
    }
}
