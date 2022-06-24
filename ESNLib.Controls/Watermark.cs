using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESNLib.Controls
{
    public class Watermark
    {
        /// <summary>
        /// Function to get the text of the control
        /// </summary>
        public Func<string> getText;
        /// <summary>
        /// Function to set the text of the control
        /// </summary>
        public Action<string> setText;
        /// <summary>
        /// Function to set hte font (fore) color of the control
        /// </summary>
        public Action<Color> setFontColor;
        
        private bool active = false;
        private bool enabled = false;
        private string watermarkText = "Type here...";
        private Color watermarkColor = SystemColors.GrayText;
        
        /// <summary>
        /// Text of the watermark
        /// </summary>
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

        /// <summary>
        /// Color of the watermark
        /// </summary>
        public Color WatermarkColor
        {
            get
            {
                return watermarkColor;
            }
            set
            {
                watermarkColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Color of the base text
        /// </summary>
        public Color TextColor { get; set; }

        public string EmptyText { get; set; } = string.Empty;

        /// <summary>
        /// Implement watermark to a text control
        /// </summary>
        public Watermark()
        {
            enabled = false;
            active = false;
            WatermarkText = "Type here...";
            WatermarkColor = SystemColors.GrayText;
            TextColor = SystemColors.ControlText;
            Invalidate();
        }

        /// <summary>
        /// Enable the watermark
        /// </summary>
        public void Enable()
        {
            enabled = true;
            active = getText() == EmptyText;
            Invalidate();
        }

        /// <summary>
        /// Disable the watermark
        /// </summary>
        public void Disable()
        {
            enabled = false;
            if(active)
            {
                active = false;
                setText(EmptyText);
                setFontColor(TextColor);
            }
        }

        /// <summary>
        /// Hide the watermark
        /// </summary>
        private void RemoveWatermark()
        {
            active = false;
            setText(EmptyText);
            setFontColor(TextColor);
        }

        /// <summary>
        /// Display the watermark
        /// </summary>
        private void ApplyWatermark()
        {
            active = true;
            setText(WatermarkText);
            setFontColor(WatermarkColor);
        }

        /// <summary>
        /// Update when WatermarkText changed on runtime
        /// </summary>
        public void Invalidate()
        {
            if (!enabled)
                return;

            if (active)
            {
                ApplyWatermark();
            }
        }
        
        /// <summary>
        /// Call this on the onFocusEnter event
        /// </summary>
        public void onFocusEnter()
        {
            if (!enabled)
                return;

            // When enterring focus, clear the watermark
            if(active)
            {
                RemoveWatermark();
            }
        }

        /// <summary>
        /// Call this on the onFocusLost event
        /// </summary>
        public void onFocusLost()
        {
            if (!enabled)
                return;

            // When leaving focus, if active or empty, set watermark
            if (active || getText() == EmptyText)
            {
                ApplyWatermark();
            }
        }
    }
}
