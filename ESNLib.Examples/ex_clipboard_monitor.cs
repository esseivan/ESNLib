using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESNLib.Examples
{
    public partial class ex_clipboard_monitor : Form
    {
        public ex_clipboard_monitor()
        {
            InitializeComponent();
        }

        private void clipboardMonitor1_ClipboardChanged(object sender, Tools.WinForms.ClipboardChangedEventArgs e)
        {
            string format = e.DataObject.GetFormats(true)[0];
            Console.WriteLine(e.DataObject.GetData(format, true));
        }
    }
}
