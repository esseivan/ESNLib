using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ESNLib.Examples
{
    public partial class Form1 : Form
    {
        private string[] args;

        public List<Type> WindowsList = new List<Type>()
        {
            typeof(ex_dialog),
            typeof(ex_dialogInput),
            typeof(ex_setting_manager),
            typeof(ex_textbox_watermark),
            typeof(ex_update_checker),
            typeof(ex_watermark),
            typeof(ex_flags),
            typeof(ex_clipboard_monitor),
            typeof(ex_logger),
            typeof(ex_tools),
            typeof(ex_math),
            typeof(ex_plugins),
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            args = Environment.GetCommandLineArgs();

            Console.WriteLine("*** Arguments :");
            foreach (string arg in args)
            {
                //MessageBox.Show(arg);
                Console.WriteLine(arg);
            }
            Console.WriteLine("End of Arguments");

            if (args.Contains("INSTALLED"))
            {
                // Keep precedent arguments
                string args_line = string.Empty;
                if (args.Length > 1)
                {
                    args_line = string.Join(" ", args).Replace(" INSTALLED", "");
                }

                // Restart the exe without the "INSTALLED" argument
                Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location, args_line);
                Close();
                return;
            }

            if (args.Length > 1)
            {
                // If arg is number
                if (int.TryParse(args[1], out int index))
                {
                    RunWindow(index);
                    this.Close();
                }
            }

            listBox1.Items.AddRange(new string[]
            {
                "Dialog",
                "Dialog Input",
                "Settings Manager",
                "TextBox Watermark",
                "Update Checker",
                "Watermark",
                "Flags",
                "Clipboard monitor",
                "Logger",
                "Tools",
                "Math",
                "Plugin",
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RunWindow(listBox1.SelectedIndex);
        }

        public void RunWindow(int index)
        {
            if (index >= WindowsList.Count || index == -1)
                return;

            Form frm = (Form)Activator.CreateInstance(WindowsList[index]);
            frm.ShowDialog();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != 0)
            {
                button1.PerformClick();
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listBox1.SelectedIndex != 0)
                {
                    button1.PerformClick();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
