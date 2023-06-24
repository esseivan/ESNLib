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

        public frmChooseHeaderLinking(IEnumerable<string> properties, IEnumerable<object> objects) : this()
        {
            Init(properties, objects);
        }

        private void Init(IEnumerable<string> properties, IEnumerable<object> objects)
        {
            dataGridView1.AutoGenerateColumns = false;

            foreach (string item in properties)
            {
                var col1 = new DataGridViewTextBoxColumn();
                col1.DataPropertyName = item;
                col1.Name = item;
                dataGridView1.Columns.Add(col1);
            }

            BindingSource bs = new BindingSource();
            objects.ToList().ForEach((x) => bs.Add(x));
            dataGridView1.DataSource = bs;

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
        private DataGridView dataGridView1;


















        /************* Initialize ***************/

        private Button button1;

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(607, 150);
            this.dataGridView1.TabIndex = 1;
            // 
            // frmChooseHeaderLinking
            // 
            this.ClientSize = new System.Drawing.Size(631, 352);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmChooseHeaderLinking";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
