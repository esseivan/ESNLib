using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESNLib.Tools.WinForms
{
    public class frmChooseHeaderLinking : Form
    {
        /// <summary>
        /// The data contained in the csv
        /// </summary>
        public string Data { get; set; }

        private void Populate()
        {
            if (string.IsNullOrEmpty(Data))
            {
                throw new ArgumentNullException(nameof(Data));
            }

            // Split by lines
            string[] lines = Data.Split('\n');
            // Headers on first line
            string[] headers = lines[0].Split(',');
            string[] elements = lines.Skip(1).ToArray();
            elements = elements.Take(4).ToArray(); // Only display the first four


        }


















        /************* Initialize ***************/

        private Button button1;

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(477, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Class1
            // 
            this.ClientSize = new System.Drawing.Size(631, 352);
            this.Controls.Add(this.button1);
            this.Name = "Class1";
            this.ResumeLayout(false);

        }
    }
}
