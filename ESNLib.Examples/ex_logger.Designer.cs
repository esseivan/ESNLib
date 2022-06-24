namespace ESNLib.Examples
{
    partial class ex_logger
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
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.textboxWatermark2 = new ESNLib.Controls.TextboxWatermark();
            this.textboxWatermark1 = new ESNLib.Controls.TextboxWatermark();
            this.richTextboxWatermark1 = new ESNLib.Controls.RichTextboxWatermark();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(403, 273);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Write to log";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Pick path";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 122);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Enable log";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 41);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(12, 68);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 6;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(12, 95);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(93, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Open file";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(12, 151);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(121, 21);
            this.comboBox4.TabIndex = 6;
            // 
            // textboxWatermark2
            // 
            this.textboxWatermark2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark2.Location = new System.Drawing.Point(139, 151);
            this.textboxWatermark2.Name = "textboxWatermark2";
            this.textboxWatermark2.Size = new System.Drawing.Size(131, 20);
            this.textboxWatermark2.TabIndex = 8;
            this.textboxWatermark2.TextColor = System.Drawing.SystemColors.ControlText;
            this.textboxWatermark2.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark2.WatermarkText = "Custom level";
            // 
            // textboxWatermark1
            // 
            this.textboxWatermark1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.Location = new System.Drawing.Point(139, 69);
            this.textboxWatermark1.Name = "textboxWatermark1";
            this.textboxWatermark1.Size = new System.Drawing.Size(131, 20);
            this.textboxWatermark1.TabIndex = 8;
            this.textboxWatermark1.TextColor = System.Drawing.SystemColors.ControlText;
            this.textboxWatermark1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.WatermarkText = "Custom suffix";
            // 
            // richTextboxWatermark1
            // 
            this.richTextboxWatermark1.baseText = "Type here...";
            this.richTextboxWatermark1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.richTextboxWatermark1.Location = new System.Drawing.Point(12, 178);
            this.richTextboxWatermark1.Name = "richTextboxWatermark1";
            this.richTextboxWatermark1.Size = new System.Drawing.Size(385, 118);
            this.richTextboxWatermark1.TabIndex = 3;
            this.richTextboxWatermark1.Text = "";
            this.richTextboxWatermark1.TextColor = System.Drawing.SystemColors.ControlText;
            this.richTextboxWatermark1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.richTextboxWatermark1.WatermarkText = "Type here...";
            // 
            // ex_logger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 308);
            this.Controls.Add(this.textboxWatermark2);
            this.Controls.Add(this.textboxWatermark1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.richTextboxWatermark1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ex_logger";
            this.Text = "ex_logger";
            this.Load += new System.EventHandler(this.ex_logger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button2;
        private ESNLib.Controls.RichTextboxWatermark richTextboxWatermark1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button3;
        private ESNLib.Controls.TextboxWatermark textboxWatermark1;
        private System.Windows.Forms.ComboBox comboBox4;
        private ESNLib.Controls.TextboxWatermark textboxWatermark2;
    }
}