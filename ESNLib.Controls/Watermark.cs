using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESNLib.Controls
{
    /// <summary>
    /// A watermark is a default text that is displayed when the placeholder is empty.
    /// With this class, you can apply a watermark to any text-based item.
    /// </summary>
    public class Watermark
    {
        /// <summary>
        /// Function to get the text of the control. Required
        /// </summary>
        public Func<string> getText;
        /// <summary>
        /// Function to set the text of the control. Required
        /// </summary>
        public Action<string> setText;
        /// <summary>
        /// Function to set the font (fore) color of the parent. Optionnal
        /// </summary>
        public Action<Color> setFontColor = null;
        
        // Private variables
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

        /// <summary>
        /// What text is considered empty. Leave <c>string.Empty</c> if you're unsure
        /// </summary>
        public string EmptyText { get; set; } = string.Empty;

        /// <summary>
        /// Disabled by default, call the <c>Enable()</c> function
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
                setFontColor?.Invoke(TextColor);
            }
        }

        /// <summary>
        /// Hide the watermark
        /// </summary>
        private void RemoveWatermark()
        {
            active = false;
            setText(EmptyText);
            setFontColor?.Invoke(TextColor);
        }

        /// <summary>
        /// Display the watermark
        /// </summary>
        private void ApplyWatermark()
        {
            active = true;
            setText(WatermarkText);
            setFontColor?.Invoke(WatermarkColor);
        }

        /// <summary>
        /// Update when WatermarkText is changed on runtime
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
        /// Call this when the parent onFocusEnter event is trigerred. Clear the watermark
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
        /// Call this when the parent onFocusEnter event is trigerred. Apply the watermark if required
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
