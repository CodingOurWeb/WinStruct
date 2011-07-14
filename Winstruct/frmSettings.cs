using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Winstruct
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();

            txtProjectName.Text = Properties.Settings.Default.defaultProjectName;
            txtDropboxUserName.Text = Properties.Settings.Default.dropboxUsername;
            txtDropboxPassword.Text = Properties.Settings.Default.dropboxPassword;
            cbDropboxEnabled.Checked = Properties.Settings.Default.dropboxEnabled;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.defaultProjectName = txtProjectName.Text;
            Properties.Settings.Default.dropboxEnabled = cbDropboxEnabled.Checked;
            Properties.Settings.Default.dropboxUsername = txtDropboxUserName.Text;
            Properties.Settings.Default.dropboxPassword = txtDropboxPassword.Text;

            if (cbDropboxEnabled.Checked && (txtDropboxUserName.Text.Length < 4 || txtDropboxPassword.Text.Length < 4))
            {
                MessageBox.Show("You tried to enabled dropbox syncing but have not specified a correct username and/or password!", "Winstruct", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Properties.Settings.Default.Save();
                this.Close();
            }
        }

        private void cbDropboxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDropboxEnabled.Checked)
            {
                groupBoxDropbox.Height = 150;
            }
            else
            {
                groupBoxDropbox.Height = 61;
            }
        }
    }
}
