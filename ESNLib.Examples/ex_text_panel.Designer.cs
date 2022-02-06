namespace Examples
{
    partial class ex_text_panel
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textPanel1 = new EsseivaN.Controls.TextPanel();
            this.textboxWatermark1 = new EsseivaN.Controls.TextboxWatermark();
            this.textboxWatermark2 = new EsseivaN.Controls.TextboxWatermark();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 168);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(150, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textPanel1
            // 
            this.textPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textPanel1.Location = new System.Drawing.Point(12, 12);
            this.textPanel1.Name = "textPanel1";
            this.textPanel1.Size = new System.Drawing.Size(150, 150);
            this.textPanel1.TabIndex = 2;
            this.textPanel1.Clipboard_Delete += new System.EventHandler(this.textPanel1_Clipboard_Delete);
            this.textPanel1.Clipboard_Load += new System.EventHandler(this.textPanel1_Clipboard_Load);
            this.textPanel1.Clipboard_Show += new System.EventHandler(this.textPanel1_Clipboard_Show);
            // 
            // textboxWatermark1
            // 
            this.textboxWatermark1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.Location = new System.Drawing.Point(12, 194);
            this.textboxWatermark1.Name = "textboxWatermark1";
            this.textboxWatermark1.Size = new System.Drawing.Size(150, 20);
            this.textboxWatermark1.TabIndex = 3;
            this.textboxWatermark1.TextColor = System.Drawing.SystemColors.ControlText;
            this.textboxWatermark1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.WatermarkText = "Label";
            this.textboxWatermark1.TextChanged += new System.EventHandler(this.textboxWatermark1_TextChanged);
            // 
            // textboxWatermark2
            // 
            this.textboxWatermark2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark2.Location = new System.Drawing.Point(12, 220);
            this.textboxWatermark2.Name = "textboxWatermark2";
            this.textboxWatermark2.Size = new System.Drawing.Size(150, 20);
            this.textboxWatermark2.TabIndex = 4;
            this.textboxWatermark2.TextColor = System.Drawing.SystemColors.ControlText;
            this.textboxWatermark2.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark2.WatermarkText = "Try paste";
            // 
            // ex_text_panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 271);
            this.Controls.Add(this.textboxWatermark2);
            this.Controls.Add(this.textboxWatermark1);
            this.Controls.Add(this.textPanel1);
            this.Controls.Add(this.textBox1);
            this.Name = "ex_text_panel";
            this.Text = "text_panel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private EsseivaN.Controls.TextPanel textPanel1;
        private EsseivaN.Controls.TextboxWatermark textboxWatermark1;
        private EsseivaN.Controls.TextboxWatermark textboxWatermark2;
    }
}