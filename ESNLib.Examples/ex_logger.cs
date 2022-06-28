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
            richTextboxWatermark1.Text = richTextboxWatermark1.Text.Trim();
            Write(richTextboxWatermark1.Text);
            richTextboxWatermark1.SelectAll();
            richTextboxWatermark1.Focus();
        }

        private void Write(string data, Logger.LogLevels level)
        {
            if (logger == null)
            {
                MessageBox.Show("Enable logger before writing in log !");
                return;
            }

            if (cbLoglevel.SelectedIndex == cbLoglevel.Items.Count - 1)
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
            Write(data, (Logger.LogLevels)Enum.Parse(
                typeof(Logger.LogLevels),
                cbLoglevel.SelectedItem.ToString()));
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
            logger.FilenameMode = (Logger.FilenamesModes)Enum.Parse(
                typeof(Logger.FilenamesModes),
                cbFilename.SelectedItem.ToString());

            logger.PrefixMode = (Logger.PrefixModes)Enum.Parse(
                typeof(Logger.PrefixModes),
                cbPrefix.SelectedItem.ToString());

            logger.WriteMode = (Logger.WriteModes)Enum.Parse(
                typeof(Logger.WriteModes),
                cbWrite.SelectedItem.ToString());

            logger.Enable();
        }

        private void ex_logger_Load(object sender, EventArgs e)
        {
            cbFilename.Items.AddRange(Enum.GetNames(typeof(Logger.FilenamesModes)));
            cbPrefix.Items.AddRange(Enum.GetNames(typeof(Logger.PrefixModes)));
            cbWrite.Items.AddRange(Enum.GetNames(typeof(Logger.WriteModes)));
            cbLoglevel.Items.AddRange(Enum.GetNames(typeof(Logger.LogLevels)));
            cbLoglevel.Items.Add("Custom");
            cbFilename.SelectedIndex = 3;
            cbPrefix.SelectedIndex = 1;
            cbWrite.SelectedIndex = cbLoglevel.SelectedIndex = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            logger.Disable();
        }

        private void richTextboxWatermark1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode.HasFlag(Keys.Enter)))
            {
                e.Handled = true;
                button1.PerformClick();
            }
        }
    }
}
