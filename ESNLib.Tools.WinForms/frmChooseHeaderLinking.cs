﻿using System;
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

        public void Init<T>(IEnumerable<T> items, IEnumerable<string> headers)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataError += DataGridView1_DataError;

            // Create table
            var properties = typeof(T).GetProperties();

            List<LineItem> myList = new List<LineItem>();
            for (int i = 0; i < properties.Count(); i++)
            {
                myList.Insert(i, new LineItem() { PropertyName = properties[i].Name, HeaderName = "?", });

                if (items.Count() >= 1)
                {
                    myList[i].Item1 = properties[i].GetValue(items.ElementAt(0))?.ToString();
                    if (items.Count() >= 2)
                    {
                        myList[i].Item2 = properties[i].GetValue(items.ElementAt(1))?.ToString();
                        if (items.Count() >= 3)
                        {
                            myList[i].Item3 = properties[i].GetValue(items.ElementAt(2))?.ToString();
                        }
                    }
                }
            }

            // Create columns
            // Class properties | Header linking | Item1 | Item2 ...
            var colProp = new DataGridViewTextBoxColumn()
            {
                ReadOnly = true,
                Name = nameof(T),
                DataPropertyName = "PropertyName"
            };
            dataGridView1.Columns.Add(colProp);

            headers = headers.Prepend("?").ToArray();
            var colHeader = new DataGridViewComboBoxColumn()
            {
                Name = "Header link",
                DataPropertyName = "HeaderName",
                DataSource = headers,
            };
            dataGridView1.Columns.Add(colHeader);

            int itemCounter = 1;
            foreach (T item in items)
            {
                var col1 = new DataGridViewTextBoxColumn
                {
                    Name = $"Item{itemCounter}",
                    ReadOnly = true,
                    DataPropertyName = $"Item{itemCounter}",
                };
                itemCounter++;
                dataGridView1.Columns.Add(col1);
            }

            BindingSource bs = new BindingSource();
            myList.ToList().ForEach((x) => bs.Add(x));
            dataGridView1.DataSource = bs;
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine(e.ToString());
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
            this.button1.Location = new System.Drawing.Point(463, 317);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Accept";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(544, 317);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Verify that the headers are not duplicate

        }
    }

        internal class LineItem
    {
        public LineItem() { }

        public LineItem(string propertyName, string headerName, string[] items)
        {
            PropertyName = propertyName;
            HeaderName = headerName;
            Item1 = items[0];
            Item2 = items[1];
            Item3 = items[2];
        }

        public string PropertyName { get; set; }
        public string HeaderName { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
    }

}
