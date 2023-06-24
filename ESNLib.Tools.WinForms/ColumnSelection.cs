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

        public ColumnSelection()
        {
            InitializeComponent();
            PopulateLines();
        }

        private void PopulateLines()
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

        public void AddObjects<T>(IEnumerable<object> obj)
        {
            var prop = typeof(T).GetProperties();
        }
    }
}
