/*
 * File     : DialogInputForm.cs
 * Author   : Esseiva Nicolas
 * Date     : 18.11.2017
 *
 * Allows the user to enter an input and
 * use the result in the software
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ESNLib.Controls
{
    /// <summary>
    /// Dialog window form
    /// </summary>
    internal partial class DialogInputForm : Form
    {
        private new static Dialog.DialogResult DialogResult;
        private static Dialog.ButtonType Btn1,
            Btn2,
            Btn3;
        private static string custom1_t,
            custom2_t,
            custom3_t;
        public string Result { get; set; }
        private bool Input { get; set; } = false;

        const double MaximumSizeRatio = 2d / 3d;

        #region initialization

        /// <summary>
        /// Dialog input window
        /// </summary>
        public DialogInputForm()
        {
            InitializeComponent();
        }

        public static void SetButton(int button, string text)
        {
            switch (button)
            {
                case 1:
                    custom1_t = text;
                    break;
                case 2:
                    custom2_t = text;
                    break;
                case 3:
                    custom3_t = text;
                    break;
                case 255:
                    custom1_t = custom2_t = custom3_t = text;
                    break;
                default:
                    break;
            }
        }

        public static void RemoveButton(int button)
        {
            switch (button)
            {
                case 1:
                    custom1_t = string.Empty;
                    break;
                case 2:
                    custom2_t = string.Empty;
                    break;
                case 3:
                    custom3_t = string.Empty;
                    break;
                case 255:
                    custom1_t = custom2_t = custom3_t = string.Empty;
                    break;
                default:
                    break;
            }
        }

        public static Dialog.ShowDialogResult ShowDialog(
            string Message,
            string Title = "Title",
            string DefaultInput = "",
            bool Input = false,
            Dialog.ButtonType Button1 = Dialog.ButtonType.OK,
            Dialog.ButtonType Button2 = Dialog.ButtonType.Cancel,
            Dialog.ButtonType Button3 = Dialog.ButtonType.None,
            Dialog.DialogIcon Icon = Dialog.DialogIcon.None
        )
        {
            Btn1 = Button1;
            Btn2 = Button2;
            Btn3 = Button3;

            DialogInputForm dialogForm = new DialogInputForm();

            // Button 1 configuration
            if (Btn1 == Dialog.ButtonType.None)
            {
                dialogForm.button1.Visible = false;
                dialogForm.button1.Enabled = false;
            }
            else
            {
                dialogForm.CancelButton = dialogForm.button1;
                if (Btn1 >= Dialog.ButtonType.Custom1)
                {
                    dialogForm.button1.Text = GetTextForCustom(Btn1);
                }
                else
                {
                    dialogForm.button1.Text = Btn1.ToString();
                }
            }

            // Button 2
            if (Btn2 == Dialog.ButtonType.None)
            {
                dialogForm.button2.Visible = false;
                dialogForm.button2.Enabled = false;
            }
            else
            {
                dialogForm.CancelButton = dialogForm.button2;
                if (Btn2 >= Dialog.ButtonType.Custom1)
                {
                    dialogForm.button2.Text = GetTextForCustom(Btn2);
                }
                else
                {
                    dialogForm.button2.Text = Btn2.ToString();
                }
            }

            // Button 3
            if (Btn3 == Dialog.ButtonType.None)
            {
                dialogForm.button3.Visible = false;
                dialogForm.button3.Enabled = false;
            }
            else
            {
                dialogForm.CancelButton = dialogForm.button3;
                if (Btn3 >= Dialog.ButtonType.Custom1)
                {
                    dialogForm.button3.Text = GetTextForCustom(Btn3);
                }
                else
                {
                    dialogForm.button3.Text = Btn3.ToString();
                }
            }

            DialogResult = Dialog.DialogResult.None;

            dialogForm.Text = Title;
            dialogForm.label_text.Text = Message;
            dialogForm.Input = Input;
            dialogForm.txt_userInput.Text = DefaultInput;

            dialogForm.pictureBox1.Visible = true;

            Icon t_icon = null;
            switch (Icon)
            {
                case Dialog.DialogIcon.None:
                    dialogForm.pictureBox1.Visible = false;
                    break;
                case Dialog.DialogIcon.Application:
                    t_icon = SystemIcons.Application;
                    break;
                case Dialog.DialogIcon.Asterisk:
                    t_icon = SystemIcons.Asterisk;
                    break;
                case Dialog.DialogIcon.Error:
                    t_icon = SystemIcons.Error;
                    break;
                case Dialog.DialogIcon.Hand:
                    t_icon = SystemIcons.Hand;
                    break;
                case Dialog.DialogIcon.Exclamation:
                    t_icon = SystemIcons.Exclamation;
                    break;
                case Dialog.DialogIcon.Shield:
                    t_icon = SystemIcons.Shield;
                    break;
                case Dialog.DialogIcon.Question:
                    t_icon = SystemIcons.Question;
                    break;
                case Dialog.DialogIcon.Warning:
                    t_icon = SystemIcons.Warning;
                    break;
                case Dialog.DialogIcon.Information:
                    t_icon = SystemIcons.Information;
                    break;
                case Dialog.DialogIcon.WinLogo:
                    t_icon = SystemIcons.WinLogo;
                    break;
                default:
                    break;
            }
            if (t_icon != null)
            {
                dialogForm.pictureBox1.Image = t_icon.ToBitmap();
            }

            dialogForm.ShowDialog();
            return new Dialog.ShowDialogResult(dialogForm.Result, DialogResult);
        }

        private static string GetTextForCustom(Dialog.ButtonType buttonType)
        {
            switch (buttonType)
            {
                case Dialog.ButtonType.Custom1:
                    return custom1_t;
                case Dialog.ButtonType.Custom2:
                    return custom2_t;
                case Dialog.ButtonType.Custom3:
                    return custom3_t;
                default:
                    return string.Empty;
            }
        }

        #endregion

        #region Main

        // Execute on load
        private void DialogInputForm_Load(object sender, EventArgs e)
        {
            panelInput.Visible = Input; // Is user input requested
            if (Input)
            {
                txt_userInput.Focus(); // If so, focus the userinput
                txt_userInput.Select(txt_userInput.TextLength, 0); // And make sure cursor is in last position
            }
            else
            {
                button1.Focus(); // Otherwise, focus first button
            }
        }

        #endregion

        private void DialogInputForm_KeyDown(object sender, KeyEventArgs e)
        {
            // If no cancel button defined and escape is pressed, close the window
            if (e.KeyCode == Keys.Escape && CancelButton == null)
            {
                Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // button 1 clicked
            DialogResult = (Dialog.DialogResult)Btn1;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // button 2 clicked
            DialogResult = (Dialog.DialogResult)Btn2;
            Close();
        }

        private void Button3_CLick(object sender, EventArgs e)
        {
            // button 3 clicked
            DialogResult = (Dialog.DialogResult)Btn3;
            Close();
        }

        private void DialogInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save the user input when closing
            Result = txt_userInput.Text;
        }
    }
}
