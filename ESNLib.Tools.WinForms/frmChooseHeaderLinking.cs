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

        public frmChooseHeaderLinking()
        {
            InitializeComponent();
        }

        public frmChooseHeaderLinking(IEnumerable<string> properties, IEnumerable<object> objects)
            : this()
        {
            Init(properties, objects);
        }

        private void Init(IEnumerable<string> properties, IEnumerable<object> objects)
        {
            //dataGridView1.AutoGenerateColumns = false;

            //foreach (string item in properties)
            //{
            //    var col1 = new DataGridViewTextBoxColumn();
            //    col1.DataPropertyName = item;
            //    col1.Name = item;
            //    dataGridView1.Columns.Add(col1);
            //}

            //BindingSource bs = new BindingSource();
            //objects.ToList().ForEach((x) => bs.Add(x));
            //dataGridView1.DataSource = bs;
        }

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

        private Button button2;
        private Examples.ColumnSelection columnSelection1;
        private Examples.ColumnSelection columnSelection2;

        /************* Initialize ***************/

        private Button button1;

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.columnSelection2 = new ESNLib.Examples.ColumnSelection();
            this.columnSelection1 = new ESNLib.Examples.ColumnSelection();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(405, 317);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Accept";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(486, 317);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // columnSelection2
            // 
            this.columnSelection2.AutoSize = true;
            this.columnSelection2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.columnSelection2.Location = new System.Drawing.Point(114, 12);
            this.columnSelection2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.columnSelection2.Name = "columnSelection2";
            this.columnSelection2.Size = new System.Drawing.Size(87, 119);
            this.columnSelection2.TabIndex = 1;
            // 
            // columnSelection1
            // 
            this.columnSelection1.AutoSize = true;
            this.columnSelection1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.columnSelection1.Location = new System.Drawing.Point(12, 12);
            this.columnSelection1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.columnSelection1.Name = "columnSelection1";
            this.columnSelection1.Size = new System.Drawing.Size(102, 119);
            this.columnSelection1.TabIndex = 1;
            // 
            // frmChooseHeaderLinking
            // 
            this.ClientSize = new System.Drawing.Size(631, 352);
            this.Controls.Add(this.columnSelection2);
            this.Controls.Add(this.columnSelection1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmChooseHeaderLinking";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
