namespace Examples
{
    partial class ex_textbox_watermark
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextboxWatermark1 = new EsseivaN.Controls.RichTextboxWatermark();
            this.textboxWatermark1 = new EsseivaN.Controls.TextboxWatermark();
            this.textboxWatermark3 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Pick Color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.textboxWatermark3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 119);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(125, 45);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(168, 20);
            this.textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(104, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(15, 20);
            this.textBox1.TabIndex = 1;
            // 
            // richTextboxWatermark1
            // 
            this.richTextboxWatermark1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.richTextboxWatermark1.Location = new System.Drawing.Point(370, 56);
            this.richTextboxWatermark1.Name = "richTextboxWatermark1";
            this.richTextboxWatermark1.Size = new System.Drawing.Size(231, 76);
            this.richTextboxWatermark1.TabIndex = 4;
            this.richTextboxWatermark1.Text = "";
            this.richTextboxWatermark1.TextColor = System.Drawing.SystemColors.ControlText;
            this.richTextboxWatermark1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.richTextboxWatermark1.WatermarkText = "Type here...";
            // 
            // textboxWatermark1
            // 
            this.textboxWatermark1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.Location = new System.Drawing.Point(370, 28);
            this.textboxWatermark1.Name = "textboxWatermark1";
            this.textboxWatermark1.Size = new System.Drawing.Size(231, 20);
            this.textboxWatermark1.TabIndex = 0;
            this.textboxWatermark1.TextColor = System.Drawing.Color.Empty;
            this.textboxWatermark1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.WatermarkText = "Type here...";
            // 
            // textboxWatermark3
            // 
            this.textboxWatermark3.Location = new System.Drawing.Point(6, 19);
            this.textboxWatermark3.Name = "textboxWatermark3";
            this.textboxWatermark3.Size = new System.Drawing.Size(100, 20);
            this.textboxWatermark3.TabIndex = 4;
            this.textboxWatermark3.TextChanged += new System.EventHandler(this.textboxWatermark3_TextChanged);
            // 
            // ex_textbox_watermark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 140);
            this.Controls.Add(this.richTextboxWatermark1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textboxWatermark1);
            this.Name = "ex_textbox_watermark";
            this.Text = "ex_textbox_watermark";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EsseivaN.Controls.TextboxWatermark textboxWatermark1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private EsseivaN.Controls.RichTextboxWatermark richTextboxWatermark1;
        private System.Windows.Forms.TextBox textboxWatermark3;
    }
}