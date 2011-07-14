namespace Winstruct
{
    partial class frmContentManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContentManager));
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.btnFileAdd = new System.Windows.Forms.Button();
            this.btnFileDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtShortcut = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboMode = new System.Windows.Forms.ComboBox();
            this.btnFileSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstFiles
            // 
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(12, 12);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(136, 394);
            this.lstFiles.Sorted = true;
            this.lstFiles.TabIndex = 5;
            this.lstFiles.Click += new System.EventHandler(this.lstFiles_Click);
            // 
            // btnFileAdd
            // 
            this.btnFileAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnFileAdd.Image")));
            this.btnFileAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFileAdd.Location = new System.Drawing.Point(50, 412);
            this.btnFileAdd.Name = "btnFileAdd";
            this.btnFileAdd.Size = new System.Drawing.Size(26, 26);
            this.btnFileAdd.TabIndex = 6;
            this.btnFileAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFileAdd.UseVisualStyleBackColor = true;
            this.btnFileAdd.Click += new System.EventHandler(this.btnFileAdd_Click);
            // 
            // btnFileDelete
            // 
            this.btnFileDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnFileDelete.Image")));
            this.btnFileDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFileDelete.Location = new System.Drawing.Point(79, 412);
            this.btnFileDelete.Name = "btnFileDelete";
            this.btnFileDelete.Size = new System.Drawing.Size(26, 26);
            this.btnFileDelete.TabIndex = 7;
            this.btnFileDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFileDelete.UseVisualStyleBackColor = true;
            this.btnFileDelete.Click += new System.EventHandler(this.btnFileDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(163, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(419, 20);
            this.txtName.TabIndex = 0;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Shortcut:";
            // 
            // txtShortcut
            // 
            this.txtShortcut.Location = new System.Drawing.Point(163, 70);
            this.txtShortcut.Name = "txtShortcut";
            this.txtShortcut.Size = new System.Drawing.Size(232, 20);
            this.txtShortcut.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(416, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Mode:";
            // 
            // cboMode
            // 
            this.cboMode.FormattingEnabled = true;
            this.cboMode.Items.AddRange(new object[] {
            "Custom Text",
            "URL"});
            this.cboMode.Location = new System.Drawing.Point(419, 70);
            this.cboMode.Name = "cboMode";
            this.cboMode.Size = new System.Drawing.Size(163, 21);
            this.cboMode.TabIndex = 2;
            // 
            // btnFileSave
            // 
            this.btnFileSave.Image = ((System.Drawing.Image)(resources.GetObject("btnFileSave.Image")));
            this.btnFileSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFileSave.Location = new System.Drawing.Point(507, 411);
            this.btnFileSave.Name = "btnFileSave";
            this.btnFileSave.Size = new System.Drawing.Size(75, 27);
            this.btnFileSave.TabIndex = 4;
            this.btnFileSave.Text = "Save...";
            this.btnFileSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFileSave.UseVisualStyleBackColor = true;
            this.btnFileSave.Click += new System.EventHandler(this.btnFileSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Content:";
            // 
            // txtContent
            // 
            this.txtContent.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContent.Location = new System.Drawing.Point(163, 113);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(419, 293);
            this.txtContent.TabIndex = 3;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(140, 418);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(38, 13);
            this.lblTemplate.TabIndex = 27;
            this.lblTemplate.Text = "Name:";
            // 
            // frmContentManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 443);
            this.Controls.Add(this.lblTemplate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.btnFileSave);
            this.Controls.Add(this.cboMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtShortcut);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnFileDelete);
            this.Controls.Add(this.btnFileAdd);
            this.Controls.Add(this.lstFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmContentManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File Contents Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Button btnFileAdd;
        private System.Windows.Forms.Button btnFileDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtShortcut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboMode;
        private System.Windows.Forms.Button btnFileSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label lblTemplate;
    }
}