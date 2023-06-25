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
    public partial class ColumnSelection : UserControl
    {
        /// <summary>
        /// Number of elements to display
        /// </summary>
        public static int numElements = 4;

        private const int step = 19;

        [
            Browsable(true),
            EditorBrowsable(EditorBrowsableState.Always),
            Category("misc"),
            Description("Use combobox instead of textbox")
        ]
        public bool UseCombobox { get; set; } = false;

        public ColumnSelection()
        {
            InitializeComponent();
        }

        private void ColumnSelection_Load(object sender, EventArgs e)
        {
            if (UseCombobox)
                CreateComboboxes();
            else
                CreateTextboxes();
        }

        private void CreateTextboxes()
        {
            for (int i = 0; i < numElements; i++)
            {
                TextBox newLine = new TextBox()
                {
                    Margin = new Padding(0, 0, 0, 3),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                    Location = new Point(0, (i + 1) * step + 20),
                    Size = new System.Drawing.Size(144, 17),
                    ReadOnly = true,
                    Name = $"textbox{i}",
                };
                this.Controls.Add(newLine);
            }
        }

        private void CreateComboboxes()
        {
            for (int i = 0; i < numElements; i++)
            {
                ComboBox newLine = new ComboBox()
                {
                    Margin = new Padding(0, 0, 0, 3),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                    Location = new Point(0, (i + 1) * step + 20),
                    Size = new System.Drawing.Size(144, 17),
                    Name = $"combobox{i}",
                };
                this.Controls.Add(newLine);
            }
        }

        public void DisableSelection()
        {
            comboBox1.Visible = false;
        }

        public void SetSelection() { }

        public void SetTypeProperties<T>()
        {
            System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();
            IEnumerable<string> names = properties.Select((x) => x.Name);
            SetObjects(names);
        }

        public void SetObjects(IEnumerable<string> objects) { }
    }
}
