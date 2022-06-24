namespace ESNLib.Examples
{
    partial class ex_dialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mL1 = new System.Windows.Forms.ComboBox();
            this.mL2 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.mL3 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.mI1 = new System.Windows.Forms.ComboBox();
            this.mMsg = new ESNLib.Controls.TextboxWatermark();
            this.mTitle = new ESNLib.Controls.TextboxWatermark();
            this.mB3 = new ESNLib.Controls.TextboxWatermark();
            this.mB2 = new ESNLib.Controls.TextboxWatermark();
            this.mB1 = new ESNLib.Controls.TextboxWatermark();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mMsg);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mI1);
            this.groupBox1.Controls.Add(this.mL1);
            this.groupBox1.Controls.Add(this.mTitle);
            this.groupBox1.Controls.Add(this.mL2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.mB3);
            this.groupBox1.Controls.Add(this.mL3);
            this.groupBox1.Controls.Add(this.mB2);
            this.groupBox1.Controls.Add(this.mB1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 202);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DialogInput";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "-";
            // 
            // mL1
            // 
            this.mL1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mL1.FormattingEnabled = true;
            this.mL1.Location = new System.Drawing.Point(145, 18);
            this.mL1.Name = "mL1";
            this.mL1.Size = new System.Drawing.Size(121, 21);
            this.mL1.TabIndex = 13;
            // 
            // mL2
            // 
            this.mL2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mL2.FormattingEnabled = true;
            this.mL2.Location = new System.Drawing.Point(145, 71);
            this.mL2.Name = "mL2";
            this.mL2.Size = new System.Drawing.Size(121, 21);
            this.mL2.TabIndex = 15;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 171);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Show messagebox";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mL3
            // 
            this.mL3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mL3.FormattingEnabled = true;
            this.mL3.Location = new System.Drawing.Point(145, 124);
            this.mL3.Name = "mL3";
            this.mL3.Size = new System.Drawing.Size(121, 21);
            this.mL3.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 41);
            this.button1.TabIndex = 20;
            this.button1.Text = "Show default";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mI1
            // 
            this.mI1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mI1.FormattingEnabled = true;
            this.mI1.Location = new System.Drawing.Point(6, 71);
            this.mI1.Name = "mI1";
            this.mI1.Size = new System.Drawing.Size(121, 21);
            this.mI1.TabIndex = 13;
            // 
            // mMsg
            // 
            this.mMsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mMsg.Location = new System.Drawing.Point(6, 19);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(121, 20);
            this.mMsg.TabIndex = 11;
            this.mMsg.Text = "Message";
            this.mMsg.TextColor = System.Drawing.SystemColors.ControlText;
            this.mMsg.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.mMsg.WatermarkText = "Message";
            // 
            // mTitle
            // 
            this.mTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mTitle.Location = new System.Drawing.Point(6, 45);
            this.mTitle.Name = "mTitle";
            this.mTitle.Size = new System.Drawing.Size(121, 20);
            this.mTitle.TabIndex = 12;
            this.mTitle.Text = "Title";
            this.mTitle.TextColor = System.Drawing.SystemColors.ControlText;
            this.mTitle.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.mTitle.WatermarkText = "Title";
            // 
            // mB3
            // 
            this.mB3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mB3.Location = new System.Drawing.Point(145, 151);
            this.mB3.Name = "mB3";
            this.mB3.Size = new System.Drawing.Size(121, 20);
            this.mB3.TabIndex = 18;
            this.mB3.Text = "_Btn3_";
            this.mB3.TextColor = System.Drawing.SystemColors.ControlText;
            this.mB3.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.mB3.WatermarkText = "Custom text Btn3";
            // 
            // mB2
            // 
            this.mB2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mB2.Location = new System.Drawing.Point(145, 98);
            this.mB2.Name = "mB2";
            this.mB2.Size = new System.Drawing.Size(121, 20);
            this.mB2.TabIndex = 16;
            this.mB2.Text = "_Btn2_";
            this.mB2.TextColor = System.Drawing.SystemColors.ControlText;
            this.mB2.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.mB2.WatermarkText = "Custom text Btn2";
            // 
            // mB1
            // 
            this.mB1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mB1.Location = new System.Drawing.Point(145, 45);
            this.mB1.Name = "mB1";
            this.mB1.Size = new System.Drawing.Size(121, 20);
            this.mB1.TabIndex = 14;
            this.mB1.Text = "_Btn1_";
            this.mB1.TextColor = System.Drawing.SystemColors.ControlText;
            this.mB1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.mB1.WatermarkText = "Custom text Btn1";
            // 
            // ex_dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 225);
            this.Controls.Add(this.groupBox1);
            this.Name = "ex_dialog";
            this.Text = "ex_dialog";
            this.Load += new System.EventHandler(this.ex_dialogInput_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ESNLib.Controls.TextboxWatermark mMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox mL1;
        private ESNLib.Controls.TextboxWatermark mTitle;
        private System.Windows.Forms.ComboBox mL2;
        private System.Windows.Forms.Button button2;
        private ESNLib.Controls.TextboxWatermark mB3;
        private System.Windows.Forms.ComboBox mL3;
        private ESNLib.Controls.TextboxWatermark mB2;
        private ESNLib.Controls.TextboxWatermark mB1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox mI1;
    }
}