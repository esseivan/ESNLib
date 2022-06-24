namespace ESNLib.Examples
{
    partial class ex_dialogInput
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
            this.mInput = new System.Windows.Forms.TextBox();
            this.mMsg = new ESNLib.Controls.TextboxWatermark();
            this.label1 = new System.Windows.Forms.Label();
            this.mL1 = new System.Windows.Forms.ComboBox();
            this.mDInput = new ESNLib.Controls.TextboxWatermark();
            this.mTitle = new ESNLib.Controls.TextboxWatermark();
            this.mL2 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.mB3 = new ESNLib.Controls.TextboxWatermark();
            this.mL3 = new System.Windows.Forms.ComboBox();
            this.mB2 = new ESNLib.Controls.TextboxWatermark();
            this.mB1 = new ESNLib.Controls.TextboxWatermark();
            this.mI1 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mI1);
            this.groupBox1.Controls.Add(this.mInput);
            this.groupBox1.Controls.Add(this.mMsg);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mL1);
            this.groupBox1.Controls.Add(this.mDInput);
            this.groupBox1.Controls.Add(this.mTitle);
            this.groupBox1.Controls.Add(this.mL2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.mB3);
            this.groupBox1.Controls.Add(this.mL3);
            this.groupBox1.Controls.Add(this.mB2);
            this.groupBox1.Controls.Add(this.mB1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 203);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DialogInput";
            // 
            // mInput
            // 
            this.mInput.Location = new System.Drawing.Point(6, 124);
            this.mInput.Name = "mInput";
            this.mInput.ReadOnly = true;
            this.mInput.Size = new System.Drawing.Size(121, 20);
            this.mInput.TabIndex = 22;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 154);
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
            // mDInput
            // 
            this.mDInput.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mDInput.Location = new System.Drawing.Point(6, 71);
            this.mDInput.Name = "mDInput";
            this.mDInput.Size = new System.Drawing.Size(121, 20);
            this.mDInput.TabIndex = 13;
            this.mDInput.Text = "Default input";
            this.mDInput.TextColor = System.Drawing.SystemColors.ControlText;
            this.mDInput.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.mDInput.WatermarkText = "Title";
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
            // mB3
            // 
            this.mB3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mB3.Location = new System.Drawing.Point(145, 151);
            this.mB3.Name = "mB3";
            this.mB3.Size = new System.Drawing.Size(121, 20);
            this.mB3.TabIndex = 18;
            this.mB3.Text = "_Btn3_";
            this.mB3.TextColor = System.Drawing.SystemColors.ControlText;
            this.mB3.WatermarkColor = System.Drawing.Color.Red;
            this.mB3.WatermarkText = "Button 3";
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
            this.mB2.WatermarkText = "Button 2";
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
            this.mB1.WatermarkText = "Button 1";
            // 
            // mI1
            // 
            this.mI1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mI1.FormattingEnabled = true;
            this.mI1.Location = new System.Drawing.Point(9, 97);
            this.mI1.Name = "mI1";
            this.mI1.Size = new System.Drawing.Size(118, 21);
            this.mI1.TabIndex = 23;
            // 
            // ex_dialogInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 224);
            this.Controls.Add(this.groupBox1);
            this.Name = "ex_dialogInput";
            this.Text = "ex_dialogInput";
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
        private ESNLib.Controls.TextboxWatermark mDInput;
        private System.Windows.Forms.TextBox mInput;
        private System.Windows.Forms.ComboBox mI1;
    }
}