using ESNLib.Tools;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ESNLib.Examples
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
            Write(richTextboxWatermark1.Text);
        }

        private void Write(string data, Logger.LogLevels level)
        {
            if (logger == null)
            {
                MessageBox.Show("Enable logger before writing in log !");
                return;
            }

            if (comboBox4.SelectedIndex == comboBox4.Items.Count - 1)
            {
                logger.Write(data, textboxWatermark2.Text);
            }
            else
            {
                logger.Write(data, level);
            }
        }

        private void Write(string data)
        {
            Write(data, (Logger.LogLevels)comboBox4.SelectedIndex);
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
            logger.CustomPrefix = textboxWatermark1.Text;
            logger.FilePath = path;
            logger.FilenameMode = (Logger.FilenamesModes)comboBox1.SelectedIndex;
            logger.PrefixMode = (Logger.PrefixModes)comboBox2.SelectedIndex;
            logger.WriteMode = (Logger.WriteModes)comboBox3.SelectedIndex;

            logger.Enable();
        }

        private void ex_logger_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Enum.GetNames(typeof(Logger.FilenamesModes)));
            comboBox2.Items.AddRange(Enum.GetNames(typeof(Logger.PrefixModes)));
            comboBox3.Items.AddRange(Enum.GetNames(typeof(Logger.WriteModes)));
            comboBox4.Items.AddRange(Enum.GetNames(typeof(Logger.LogLevels)));
            comboBox4.Items.Add("Custom");
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = comboBox3.SelectedIndex = comboBox4.SelectedIndex = 0;
        }
    }
}
