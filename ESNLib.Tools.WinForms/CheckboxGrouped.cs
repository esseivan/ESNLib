using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESNLib.Tools.WinForms
{
    /// <summary>
    /// Group checkboxes
    /// </summary>
    public class CheckboxGrouped
    {
        /// <summary>
        /// Whether only one box should be checked or multiples can be
        /// </summary>
        public bool MultipleSelection { get; set; } = false;

        /// <summary>
        /// If none can be selected
        /// </summary>
        public bool AllowNoSelection { get; set; } = false;

        /// <summary>
        /// Default enabled checkbox when AllowNoSelection is set to false. Set null to skip
        /// </summary>
        public CheckBox DefaultActive { get; set; } = null;

        /// <summary>
        /// Default enabled checkbox index when AllowNoSelection is set to false. Set -1 to skip, first checkbox will be used
        /// </summary>
        public int DefaultActiveIndex { get; set; } = -1;

        // Used to not call the method again when checked is automatically changed
        private bool isRunning = false;

        // List of checkboxes with index
        private Dictionary<int, CheckBox> boxList = new Dictionary<int, CheckBox>();

        public CheckboxGrouped()
        {

        }

        #region Collection management
        public int Add(CheckBox checkBox)
        {
            int count = boxList.Count;
            boxList.Add(count, checkBox);
            return count;
        }

        public void Add(CheckBox checkBox, int index)
        {
            boxList.Add(index, checkBox);
        }

        public void Remove(CheckBox checkBox)
        {
            boxList.Remove(boxList.Where((kv, b) => kv.Value == checkBox).FirstOrDefault().Key);
        }

        public void Remove(int index)
        {
            boxList.Remove(index);
        }
        #endregion

        /// <summary>
        /// Should be called by the checkbox event : CheckedChanged with the source
        /// </summary>
        public void Event_CheckedChanged(object sender)
        {
            if (!(sender is CheckBox))
                return;

            if (isRunning)
                return;

            Update((CheckBox)sender);
        }

        /// <summary>
        /// Check checkboxes
        /// </summary>
        public void Update(CheckBox lastChecked)
        {
            isRunning = true;
            foreach (var item in boxList)
            {
                int index = item.Key;
                CheckBox checkBox = item.Value;

                if (checkBox.Checked && !MultipleSelection && checkBox != lastChecked)
                {
                    // uncheck
                    checkBox.Checked = false;
                }
            }
            CheckNoSelection();
            isRunning = false;
        }

        /// <summary>
        /// Check that at least 1 checkbox is selected (if activated)
        /// </summary>
        public void CheckNoSelection()
        {
            if(!AllowNoSelection)
            {
                foreach (var item in boxList)
                {
                    int index = item.Key;
                    CheckBox checkBox = item.Value;

                    if(checkBox.Checked)
                    {
                        return;
                    }
                }

                if (DefaultActive != null)
                    DefaultActive.Checked = true;
                else if (DefaultActiveIndex != -1)
                    boxList[DefaultActiveIndex].Checked = true;
                else
                    boxList.FirstOrDefault().Value.Checked = true;
            }
        }

        /// <summary>
        /// Return the first selected index
        /// </summary>
        public int GetSelectedIndex()
        {
            foreach (var item in boxList)
            {
                int index = item.Key;
                CheckBox checkBox = item.Value;

                if (checkBox.Checked)
                    return index;
            }
            return -1;
        }
    }
}
