using System.Windows.Forms;

namespace ESNLib.Controls
{
    /// <summary>
    /// Highly configurable Dialog window (e.g. Windows Forms' MessageBox). Can also ask inputs to the user.
    /// </summary>
    public class Dialog
    {
        /// <summary>
        /// The configuration for the dialog window
        /// </summary>
        public DialogConfig Config { get; set; }

        /// <summary>
        /// Create a default dialog window with no configuration
        /// </summary>
        public Dialog()
        {
        }

        /// <summary>
        /// Create a dialog window with the specified configuration
        /// </summary>
        public Dialog(DialogConfig config) : this()
        {
            this.Config = config;
        }

        /// <summary>
        /// Display the dialog window to the user and await a result
        /// </summary>
        /// <returns>Result from the user. Includes optionnal user input.</returns>
        public ShowDialogResult ShowDialog()
        {
            return ShowDialog(Config);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        /// <summary>
        /// Type of buttons
        /// </summary>
        public enum ButtonType
        {
            None = 0,
            OK = 1,
            Skip = 2,
            Ignore = 3,
            Continue = 4,
            Accept = 5,
            Previous = 6,
            Next = 7,
            Cancel = 8,
            Abort = 9,
            Retry = 10,
            Yes = 11,
            No = 12,

            /// <summary>
            /// Custom button 1. Defined by CustomButton1Text in the config
            /// </summary>
            Custom1 = 253,
            /// <summary>
            /// Custom button 2. Defined by CustomButton2Text in the config
            /// </summary>
            Custom2 = 254,
            /// <summary>
            /// Custom button 3. Defined by CustomButton3Text in the config
            /// </summary>
            Custom3 = 255,
        }

        /// <summary>
        /// Type of icon
        /// </summary>
        public enum DialogIcon
        {
            None = 0,
            Application,
            Asterisk,
            Error,
            Hand,
            Exclamation,
            Shield,
            Question,
            Warning,
            Information,
            WinLogo
        }

        /// <summary>
        /// Result of the dialog
        /// </summary>
        public enum DialogResult
        {
            None = 0,
            OK = 1,
            Skip = 2,
            Ignore = 3,
            Continue = 4,
            Accept = 5,
            Previous = 6,
            Next = 7,
            Cancel = 8,
            Abort = 9,
            Retry = 10,
            Yes = 11,
            No = 12,

            /// <summary>
            /// Custom button 1. Defined by CustomButton1Text in the config
            /// </summary>
            Custom1 = 253,
            /// <summary>
            /// Custom button 2. Defined by CustomButton2Text in the config
            /// </summary>
            Custom2 = 254,
            /// <summary>
            /// Custom button 3. Defined by CustomButton3Text in the config
            /// </summary>
            Custom3 = 255,
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Show dialog with config class
        /// </summary>
        public static ShowDialogResult ShowDialog(DialogConfig Config)
        {
            // Set custom buttons
            DialogInputForm.SetButton(1, Config.CustomButton1Text);
            DialogInputForm.SetButton(2, Config.CustomButton2Text);
            DialogInputForm.SetButton(3, Config.CustomButton3Text);

            // Show dialog
            return DialogInputForm.ShowDialog(Config.Message,
                Config.Title,
                Config.DefaultInput,
                Config.Input,
                Config.Button1,
                Config.Button2,
                Config.Button3,
                Config.Icon);
        }

        /// <summary>
        /// Show dialog with config parameters
        /// </summary>
        public static ShowDialogResult ShowDialog(string Message,
            string Title = "Information",
            string DefaultInput = "",
            bool Input = false,
            ButtonType Btn1 = ButtonType.OK,
            ButtonType Btn2 = ButtonType.None,
            ButtonType Btn3 = ButtonType.None,
            DialogIcon Icon = DialogIcon.None,
            string CB1_Text = "Custom1",
            string CB2_Text = "Custom2",
            string CB3_Text = "Custom3")
        {
            // Set custom buttons
            DialogInputForm.SetButton(1, CB1_Text);
            DialogInputForm.SetButton(2, CB2_Text);
            DialogInputForm.SetButton(3, CB3_Text);

            // Show dialog
            return DialogInputForm.ShowDialog(Message,
                Title,
                DefaultInput,
                Input,
                Btn1,
                Btn2,
                Btn3,
                Icon);
        }

        /// <summary>
        /// Config of the dialog
        /// </summary>
        public class DialogConfig
        {
            /// <summary>
            /// The main message displayed in the window
            /// </summary>
            public string Message { get; set; } = string.Empty;
            /// <summary>
            /// Title of the window
            /// </summary>
            public string Title { get; set; } = "Information";
            /// <summary>
            /// The default text in the textbox
            /// </summary>
            public string DefaultInput { get; set; } = "";
            /// <summary>
            /// Wether a text input is requested to the user (textbox visible)
            /// </summary>
            public bool Input { get; set; } = false;
            /// <summary>
            /// Leftmost button
            /// </summary>
            public ButtonType Button1 { get; set; } = ButtonType.OK;
            /// <summary>
            /// Middle button
            /// </summary>
            public ButtonType Button2 { get; set; } = ButtonType.None;
            /// <summary>
            /// Rightmost button
            /// </summary>
            public ButtonType Button3 { get; set; } = ButtonType.None;
            /// <summary>
            /// Icon next to the message
            /// </summary>
            public DialogIcon Icon { get; set; } = DialogIcon.None;
            /// <summary>
            /// Custom button text for Custom1 button type
            /// </summary>
            public string CustomButton1Text { get; set; } = "Custom1";
            /// <summary>
            /// Custom button text for Custom2 button type
            /// </summary>
            public string CustomButton2Text { get; set; } = "Custom2";
            /// <summary>
            /// Custom button text for Custom3 button type
            /// </summary>
            public string CustomButton3Text { get; set; } = "Custom3";

            /// <summary>
            /// Create a default config
            /// </summary>
            public DialogConfig()
            {

            }

            /// <summary>
            /// Create a default config with the specified message
            /// </summary>
            public DialogConfig(string Message)
            {
                this.Message = Message;
            }

            /// <summary>
            /// Create a default config with the specified message and title
            /// </summary>
            public DialogConfig(string Message, string Title)
            {
                this.Message = Message;
                this.Title = Title;
            }

            /// <summary>
            /// Create a default config with the specified message, with specifing user input
            /// </summary>
            public DialogConfig(string Message, string Title, bool Input)
            {
                this.Message = Message;
                this.Title = Title;
                this.Input = Input;
            }
        }

        /// <summary>
        /// Result of the call of ShowDialogInput
        /// </summary>
        public struct ShowDialogResult
        {
            /// <summary>
            /// The text set by the user
            /// </summary>
            public string UserInput { get; set; }
            /// <summary>
            /// The button clicked (or window closed)
            /// </summary>
            public DialogResult DialogResult { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public ShowDialogResult(string UserInput)
            {
                this.UserInput = UserInput;
                DialogResult = DialogResult.None;
            }

            /// <summary>
            /// 
            /// </summary>
            public ShowDialogResult(string input, DialogResult dialogResult)
            {
                this.UserInput = input;
                this.DialogResult = dialogResult;
            }
        }
    }
}