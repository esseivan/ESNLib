namespace Examples
{
    partial class ex_math
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textboxWatermark1 = new EsseivaN.Controls.TextboxWatermark();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textboxWatermark1
            // 
            this.textboxWatermark1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.Location = new System.Drawing.Point(12, 12);
            this.textboxWatermark1.Name = "textboxWatermark1";
            this.textboxWatermark1.Size = new System.Drawing.Size(189, 20);
            this.textboxWatermark1.TabIndex = 0;
            this.textboxWatermark1.TextColor = System.Drawing.SystemColors.ControlText;
            this.textboxWatermark1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.WatermarkText = "Function";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 38);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // ex_math
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textboxWatermark1);
            this.Name = "ex_math";
            this.Text = "ex_math";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EsseivaN.Controls.TextboxWatermark textboxWatermark1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}