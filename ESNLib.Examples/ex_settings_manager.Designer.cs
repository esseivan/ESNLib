﻿namespace Examples
{
    partial class ex_settings_manager
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
            this.btnDel = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtData2 = new EsseivaN.Controls.TextboxWatermark();
            this.txtName = new EsseivaN.Controls.TextboxWatermark();
            this.txtIndex = new EsseivaN.Controls.TextboxWatermark();
            this.txtData1 = new EsseivaN.Controls.TextboxWatermark();
            this.SuspendLayout();
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(128, 95);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(110, 24);
            this.btnDel.TabIndex = 9;
            this.btnDel.Text = "Remove setting";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetAll.Location = new System.Drawing.Point(128, 278);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(110, 23);
            this.btnGetAll.TabIndex = 8;
            this.btnGetAll.Text = "Get all";
            this.btnGetAll.UseVisualStyleBackColor = true;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddAll.Location = new System.Drawing.Point(12, 278);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(110, 23);
            this.btnAddAll.TabIndex = 8;
            this.btnAddAll.Text = "Add all";
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Location = new System.Drawing.Point(128, 176);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(110, 96);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 176);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(110, 96);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(128, 41);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(128, 70);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(110, 23);
            this.btnGet.TabIndex = 3;
            this.btnGet.Text = "Get Setting";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(128, 122);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add Setting";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 41);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(110, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load file";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(12, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(110, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New file";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(128, 148);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update Setting";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtData2
            // 
            this.txtData2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtData2.Location = new System.Drawing.Point(12, 150);
            this.txtData2.Name = "txtData2";
            this.txtData2.Size = new System.Drawing.Size(110, 20);
            this.txtData2.TabIndex = 2;
            this.txtData2.TextColor = System.Drawing.SystemColors.ControlText;
            this.txtData2.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.txtData2.WatermarkText = "Data2";
            // 
            // txtName
            // 
            this.txtName.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtName.Location = new System.Drawing.Point(12, 98);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(110, 20);
            this.txtName.TabIndex = 2;
            this.txtName.TextColor = System.Drawing.SystemColors.ControlText;
            this.txtName.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.txtName.WatermarkText = "Name";
            // 
            // txtIndex
            // 
            this.txtIndex.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtIndex.Location = new System.Drawing.Point(12, 72);
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.Size = new System.Drawing.Size(110, 20);
            this.txtIndex.TabIndex = 2;
            this.txtIndex.TextColor = System.Drawing.SystemColors.ControlText;
            this.txtIndex.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.txtIndex.WatermarkText = "Index";
            // 
            // txtData1
            // 
            this.txtData1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtData1.Location = new System.Drawing.Point(12, 124);
            this.txtData1.Name = "txtData1";
            this.txtData1.Size = new System.Drawing.Size(110, 20);
            this.txtData1.TabIndex = 2;
            this.txtData1.TextColor = System.Drawing.SystemColors.ControlText;
            this.txtData1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.txtData1.WatermarkText = "Data1";
            // 
            // ex_settings_manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 310);
            this.Controls.Add(this.btnGetAll);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtData2);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtIndex);
            this.Controls.Add(this.txtData1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnSave);
            this.Name = "ex_settings_manager";
            this.Text = "ex_settings_manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnAdd;
        private EsseivaN.Controls.TextboxWatermark txtData2;
        private EsseivaN.Controls.TextboxWatermark txtData1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private EsseivaN.Controls.TextboxWatermark txtIndex;
        private EsseivaN.Controls.TextboxWatermark txtName;
        private System.Windows.Forms.Button btnUpdate;
    }
}