using ESNLib.Tools.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESNLib.Examples
{
    public partial class ex_tools : Form, AdminTools.IAdminForm
    {
        public string[] args { get; set; }

        public ex_tools()
        {
            InitializeComponent();
            args = Environment.GetCommandLineArgs();
        }

        public string GetAppPath()
        {
            return Application.ExecutablePath;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AdminTools.RunAsAdmin(this, "hello world");
        }

        private void Ex_tools_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.DataSource = args;
        }
    }
}
