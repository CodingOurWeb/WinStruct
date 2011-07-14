namespace Winstruct
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.groupBoxDropbox = new System.Windows.Forms.GroupBox();
            this.txtDropboxPassword = new System.Windows.Forms.TextBox();
            this.txtDropboxUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbDropboxEnabled = new System.Windows.Forms.CheckBox();
            this.groupBoxDropbox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxDropbox
            // 
            this.groupBoxDropbox.Controls.Add(this.cbDropboxEnabled);
            this.groupBoxDropbox.Controls.Add(this.txtDropboxPassword);
            this.groupBoxDropbox.Controls.Add(this.txtDropboxUserName);
            this.groupBoxDropbox.Controls.Add(this.label2);
            this.groupBoxDropbox.Controls.Add(this.label1);
            this.groupBoxDropbox.Location = new System.Drawing.Point(14, 103);
            this.groupBoxDropbox.Name = "groupBoxDropbox";
            this.groupBoxDropbox.Size = new System.Drawing.Size(558, 61);
            this.groupBoxDropbox.TabIndex = 1;
            this.groupBoxDropbox.TabStop = false;
            this.groupBoxDropbox.Text = "Dropbox";
            // 
            // txtDropboxPassword
            // 
            this.txtDropboxPassword.Location = new System.Drawing.Point(155, 98);
            this.txtDropboxPassword.Name = "txtDropboxPassword";
            this.txtDropboxPassword.PasswordChar = '*';
            this.txtDropboxPassword.Size = new System.Drawing.Size(187, 22);
            this.txtDropboxPassword.TabIndex = 4;
            // 
            // txtDropboxUserName
            // 
            this.txtDropboxUserName.Location = new System.Drawing.Point(155, 66);
            this.txtDropboxUserName.Name = "txtDropboxUserName";
            this.txtDropboxUserName.Size = new System.Drawing.Size(187, 22);
            this.txtDropboxUserName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtProjectName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(558, 80);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(155, 33);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(283, 22);
            this.txtProjectName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Default Project Name:";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(481, 394);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 27);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbDropboxEnabled
            // 
            this.cbDropboxEnabled.AutoSize = true;
            this.cbDropboxEnabled.Location = new System.Drawing.Point(16, 28);
            this.cbDropboxEnabled.Name = "cbDropboxEnabled";
            this.cbDropboxEnabled.Size = new System.Drawing.Size(232, 20);
            this.cbDropboxEnabled.TabIndex = 8;
            this.cbDropboxEnabled.Text = "Use Dropbox Storage for Templates";
            this.cbDropboxEnabled.UseVisualStyleBackColor = true;
            this.cbDropboxEnabled.CheckedChanged += new System.EventHandler(this.cbDropboxEnabled_CheckedChanged);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 433);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxDropbox);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmSettings";
            this.groupBoxDropbox.ResumeLayout(false);
            this.groupBoxDropbox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDropbox;
        private System.Windows.Forms.TextBox txtDropboxPassword;
        private System.Windows.Forms.TextBox txtDropboxUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbDropboxEnabled;
    }
}