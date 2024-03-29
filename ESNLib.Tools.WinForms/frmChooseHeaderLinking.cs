﻿using ESNLib.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESNLib.Tools.WinForms
{
    public class frmChooseHeaderLinking : Form
    {
        private const string DEFAULT_PROPERTY = " ";

        public bool Result { get; private set; } = false;
        public Dictionary<string, PropertyInfo> Output { get; private set; } = null;
        private Type myType = null;

        public frmChooseHeaderLinking()
        {
            InitializeComponent();
        }

        private List<LineItem> GenerateLineList(
            string[] headers,
            PropertyInfo[] properties,
            string[][] elements
        )
        {
            List<LineItem> myList = new List<LineItem>();

            // One line for each header. The combobox let the user select the corresponding property
            for (int i = 0; i < headers.Length; i++)
            {
                // Init the line
                myList.Insert(
                    i,
                    new LineItem() { HeaderName = headers[i], PropertyName = string.Empty }
                );

                myList[i].Item1 = elements[i][0];
                myList[i].Item2 = elements[i][1];
                myList[i].Item3 = elements[i][2];
            }

            return myList;
        }

        public void Populate<T>(string Data, Dictionary<string, PropertyInfo> defaultLink)
        {
            Dictionary<string, string> newList = null;

            if (!(defaultLink == null || defaultLink.Count == 0))
            {
                newList = new Dictionary<string, string>();
                foreach (var item in defaultLink)
                {
                    newList.Add(item.Key, item.Value.Name);
                }
            }

            Populate<T>(Data, newList);
        }

        public void Populate<T>(string Data, Dictionary<string, string> defaultLink = null)
        {
            if (string.IsNullOrEmpty(Data))
            {
                throw new ArgumentNullException(nameof(Data));
            }

            myType = typeof(T);

            // Split by lines
            string[] lines = Data.Split('\n');
            // Headers on first line
            string[] headers = lines[0].Split(',');
            string[] elements = lines.Skip(1).Where((x) => !string.IsNullOrEmpty(x)).ToArray();
            if (elements.Count() > 3)
            {
                elements = elements.Take(3).ToArray(); // Only display the first 3
            }
            int n = elements.Length;

            List<LineItem> items = new List<LineItem>();
            string[] data1,
                data2,
                data3;
            data1 = elements.ElementAtOrDefault(0)?.Split(',') ?? new string[0];
            data2 = elements.ElementAtOrDefault(1)?.Split(',') ?? new string[0];
            data3 = elements.ElementAtOrDefault(2)?.Split(',') ?? new string[0];

            bool hasLink = defaultLink != null;

            for (int i = 0; i < headers.Length; i++)
            {
                LineItem li = new LineItem()
                {
                    HeaderName = headers[i],
                    PropertyName = DEFAULT_PROPERTY,
                    Item1 = data1.ElementAtOrDefault(i),
                    Item2 = data2.ElementAtOrDefault(i),
                    Item3 = data3.ElementAtOrDefault(i),
                };

                if (hasLink && defaultLink.ContainsKey(li.HeaderName))
                {
                    li.PropertyName = defaultLink[li.HeaderName];
                }

                items.Add(li);
            }

            // Give those parameters to Init
            Init<T>(items, n);
        }

        private void Init<T>(List<LineItem> items, int elementCount)
        {
            dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            //dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;

            // Create table
            PropertyInfo[] properties = typeof(T).GetProperties();
            string[] propertiesName = properties.Select((x) => x.Name).ToArray();

            // Auto detect headers
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = 0; j < propertiesName.Length; j++)
                {
                    if (items[i].PropertyName != DEFAULT_PROPERTY) // Already set. Do not replace
                        continue;

                    if (
                        propertiesName[j].Equals(
                            items[i].HeaderName,
                            StringComparison.InvariantCultureIgnoreCase
                        )
                    )
                    {
                        // Header found in properties
                        items[i].PropertyName = propertiesName[j];
                    }
                }
            }

            // Create columns
            // Header linking | Class property | Item1 | Item2 | Item3

            // Header column
            var colHeader = new DataGridViewTextBoxColumn()
            {
                ReadOnly = true,
                Name = "Header link",
                DataPropertyName = "HeaderName",
            };
            dataGridView1.Columns.Add(colHeader);

            // Property column
            propertiesName = propertiesName.Prepend(DEFAULT_PROPERTY).ToArray();
            var colProp = new DataGridViewComboBoxColumn()
            {
                Name = typeof(T).Name,
                DataPropertyName = "PropertyName",
                DataSource = propertiesName,
            };
            dataGridView1.Columns.Add(colProp);

            // Item 1,2,3 columns
            for (int i = 0; i < elementCount; i++)
            {
                var col1 = new DataGridViewTextBoxColumn
                {
                    Name = $"Item{i + 1}",
                    ReadOnly = true,
                    DataPropertyName = $"Item{i + 1}",
                };
                dataGridView1.Columns.Add(col1);
            }

            // Add to gridview
            BindingSource bs = new BindingSource();
            items.ForEach((x) => bs.Add(x));
            dataGridView1.DataSource = bs;
        }

        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string comboboxSelectedValue = string.Empty;

            if (
                dataGridView1.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewComboBoxColumn)
            )
                comboboxSelectedValue = dataGridView1.Rows[e.RowIndex].Cells[
                    e.ColumnIndex
                ].Value.ToString();

            Console.WriteLine(comboboxSelectedValue);
        }

        /************* Initialize **********************/

        private Button btnCancel;
        private DataGridView dataGridView1;
        private Button btnAccept;

        private void InitializeComponent()
        {
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            //
            // btnAccept
            //
            this.btnAccept.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (
                        System.Windows.Forms.AnchorStyles.Bottom
                        | System.Windows.Forms.AnchorStyles.Right
                    )
                )
            );
            this.btnAccept.Location = new System.Drawing.Point(463, 317);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (
                        System.Windows.Forms.AnchorStyles.Bottom
                        | System.Windows.Forms.AnchorStyles.Right
                    )
                )
            );
            this.btnCancel.Location = new System.Drawing.Point(544, 317);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // dataGridView1
            //
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (
                        (
                            (
                                System.Windows.Forms.AnchorStyles.Top
                                | System.Windows.Forms.AnchorStyles.Bottom
                            ) | System.Windows.Forms.AnchorStyles.Left
                        ) | System.Windows.Forms.AnchorStyles.Right
                    )
                )
            );
            this.dataGridView1.ColumnHeadersHeightSizeMode = System
                .Windows
                .Forms
                .DataGridViewColumnHeadersHeightSizeMode
                .AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(607, 299);
            this.dataGridView1.TabIndex = 1;
            //
            // frmChooseHeaderLinking
            //
            this.ClientSize = new System.Drawing.Size(631, 352);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Name = "frmChooseHeaderLinking";
            this.Text = "Select link between headers and properties";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }

        /************* End of Initialiye ***************/

        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Generate dictionary header to property
            Dictionary<string, PropertyInfo> links = new Dictionary<string, PropertyInfo>();

            Dictionary<PropertyInfo, int> timesUsed = new Dictionary<PropertyInfo, int>();

            const int columnIndex = 1;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string headerName = dataGridView1.Rows[i].Cells[columnIndex - 1].Value.ToString();
                string propertyName = dataGridView1.Rows[i].Cells[columnIndex].Value.ToString();
                PropertyInfo pi = myType.GetProperty(propertyName);
                if (!links.ContainsKey(headerName))
                {
                    links.Add(headerName, pi);
                    if (pi != null)
                    {
                        if (timesUsed.ContainsKey(pi))
                        {
                            timesUsed[pi]++;
                        }
                        else
                        {
                            timesUsed[pi] = 1;
                        }
                    }
                }
            }

            var over = timesUsed.Where((x) => x.Value > 1).Select((x) => $"{x.Key.Name}:{x.Value}");

            if (over.Count() > 0)
            {
                // One property set multiple times...
                Dialog.DialogConfig dc = new Dialog.DialogConfig();
                dc.Message =
                    $"Warning ! Some properties are set multiple times !\n{string.Join("; ", over)}";
                dc.Title = "Warning";
                dc.Button1 = Dialog.ButtonType.Continue;
                dc.Button2 = Dialog.ButtonType.Custom1;
                dc.CustomButton1Text = "Edit...";
                dc.Icon = Dialog.DialogIcon.Warning;

                var result = Dialog.ShowDialog(dc);
                if (result.DialogResult != Dialog.DialogResult.Continue)
                {
                    // Abort if not continue
                    return;
                }
            }

            Output = links;
            Result = true;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = false;
            this.Close();
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
