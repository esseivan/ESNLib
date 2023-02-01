using System;
using System.Drawing;
using System.Windows.Forms;
using Model = System.ComponentModel;

namespace ESNLib.Controls
{
    public class TextboxWatermark : TextBox
    {
        private string watermarkText = "Type here...";
        private bool watermarkActive = false;
        private Color watermarkColor = SystemColors.GrayText;
        private Color textColor = SystemColors.ControlText;

        [Model.Browsable(true), Model.Description("Watermark Text to be displayed"), Model.Category("Watermark"),]
        public string WatermarkText
        {
            get
            {
                return watermarkText;
            }
            set
            {
                watermarkText = value;
                Invalidate();
            }
        }

        [Model.Browsable(true), Model.Description("Watermark Text to be displayed"), Model.Category("Watermark")]
        public Color WatermarkColor
        { get { return watermarkColor; } set { watermarkColor = value; Invalidate(); } }

        [Model.Browsable(true), Model.Description("Normal Text color"), Model.Category("Appearance")]
        public Color TextColor
        { get { return textColor; } set { textColor = value; Invalidate(); } }

        public TextboxWatermark()
        {
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            watermarkActive = base.Text == string.Empty;

            if (watermarkActive)
            {
                ForeColor = WatermarkColor;
                base.Text = WatermarkText;
            }
            else
            {
                ForeColor = textColor;
            }
        }

        [Model.Browsable(true), Model.Description("The text associated with the control"), Model.Category("Appearance")]
        public override string Text
        {
            get
            {
                if (watermarkActive && watermarkText != string.Empty)
                    return string.Empty;
                else
                    return base.Text;
            }
            set
            {
                watermarkActive = (value == string.Empty);
                ForeColor = watermarkActive ? WatermarkColor : textColor;
                base.Text = value;
            }
        }

        [Model.Browsable(false)]
        public override int TextLength
        {
            get
            {
                return watermarkActive ? 0 : base.TextLength;
            }
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            if (watermarkActive)
            {
                ForeColor = WatermarkColor;
                base.Text = WatermarkText;
            }
            base.OnInvalidated(e);
        }

        public override void ResetText()
        {
            watermarkActive = true;
            ForeColor = WatermarkColor;
            base.Text = WatermarkText;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (watermarkActive)
            {
                watermarkActive = false;
                ForeColor = textColor;
                base.Text = string.Empty;
            }

            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //e.Handled = true;

            if (watermarkActive)
            {
                watermarkActive = false;
                ForeColor = textColor;
                base.Text = string.Empty;
            }

            base.OnKeyDown(e);

            // Do the keydown then check text
            Application.DoEvents();

            if (base.Text == string.Empty)
            {
                watermarkActive = true;
                ForeColor = WatermarkColor;
                base.Text = WatermarkText;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (base.Text == string.Empty)
            {
                watermarkActive = true;
                ForeColor = WatermarkColor;
                base.Text = WatermarkText;
            }
        }
    }
}
