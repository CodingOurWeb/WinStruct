using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using DropNet;
using System.Threading;

namespace Winstruct
{
    public partial class frmMain : Form
    {
        private bool slashEntered = false;

        private Structure structure;
        private String templatePath;
        private Form progress;

        public frmMain()
        {
            InitializeComponent();
      
            txtBasePath.Text = Properties.Settings.Default.basePath;
            txtProjectName.Text = Properties.Settings.Default.defaultProjectName;
            templatePath = Application.StartupPath + "\\Templates\\";

            syncDropboxNow();
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                DialogResult result = folder.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtBasePath.Text = folder.SelectedPath + "\\";
                
                    Properties.Settings.Default.basePath = txtBasePath.Text;
                    Properties.Settings.Default.Save();   
                }
                folder.Dispose();
                folder = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CreateStructure()
        {
            try
            {
                structure = new Structure(txtStructure.Lines);
                structure.basePath = txtBasePath.Text;
                structure.projectName = txtProjectName.Text;
                structure.templatePath = this.templatePath;

                ArrayList result = structure.Parse();
                if (result.Count > 0)
                {
                    String msg = "";
                    foreach (String item in result)
                    {
                        msg += "\r\n" + item;
                    }
                    MessageBox.Show("Project structure created with exceptions!" + "\r\n" + msg, "Winstruct", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Project structure created succesfully!", "Winstruct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (InvokeRequired)
                    Invoke(new Complete(OnComplete));
            }

        }

        private void OnComplete()
        {
            progress.Close();
            progress.Dispose();
        }
        private delegate void Complete();


        private void btnCreate_Click(object sender, EventArgs e)
        {
            progress = new frmProgress();
            progress.Show();
            //progress.StartPosition = FormStartPosition.CenterParent;

            Thread t = new Thread(new ThreadStart(CreateStructure));
            t.Start();       
        }


        private void btnOpenTemplate_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Template Files|*.tpl|All Files|*.*";
            ofd.InitialDirectory = templatePath;
            ofd.Title = "Please Select a Template File";

            try
            {
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtStructure.Clear();
                    StreamReader sr = new StreamReader(ofd.FileName);
                    while (true)
                    {
                        string strLine = sr.ReadLine();
                        txtStructure.Text += strLine + "\r\n";
                        if (strLine == null) break;
                    }
                    sr.Close();
                    ofd.Dispose();
                    ofd = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = templatePath;
            sfd.Title = "Please Specify a Template File Name";

            try
            {
                DialogResult result = sfd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName);
                    sw.WriteLine(txtStructure.Text);
                    sw.Close();

                    FileInfo fi = new FileInfo(sfd.FileName);
                    byte[] fileData = File.ReadAllBytes(sfd.FileName);
                    uploadTemplateToDropbox(fi.Name, fileData);
                }
                sfd.Dispose();
                sfd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void txtStructure_KeyDown(object sender, KeyEventArgs e)
        {
            slashEntered = false;
            if (e.KeyValue == 191)
            {
                slashEntered = true;
            }
        }


        private void txtStructure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (slashEntered == true)
            {
                SendKeys.Send("\\");
                e.Handled = true;
            }
        }


        private void btnContentManager_Click(object sender, EventArgs e)
        {
            Form cm = new frmContentManager();
            cm.ShowDialog();
        }


        private void clearStructure(object sender, EventArgs e)
        {
            txtStructure.Clear();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form settings = new frmSettings();
            settings.ShowDialog();
        }


        private void syncTemplatesNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            syncDropboxNow();
        }

        /// <summary>
        /// Upload a local Template file to users' Dropbox
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>
        private void uploadTemplateToDropbox(String fileName, byte[] fileData) 
        {
            try
            {
                if (Properties.Settings.Default.dropboxEnabled)
                {
                    String dropboxTemplatePath = "\\Public\\Templates";

                    DropNetClient dc = new DropNetClient("2eie8xk4bqetn7m", "q4gjahtqu0w0le8");
                    DropNet.Models.UserLogin ul = new DropNet.Models.UserLogin();

                    ul = dc.Login(Properties.Settings.Default.dropboxUsername, Properties.Settings.Default.dropboxPassword);
                    
                    if (ul.Secret == null || ul.Token == null)
                    {
                        MessageBox.Show("Invalid Dropbox credentials specified! Synchronisation could not be performed!", "Winstruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    var folderDetails = dc.GetMetaData(dropboxTemplatePath);
                    var contents = folderDetails.Contents;
                    if (contents.Count == 0)
                    {
                        dc.CreateFolder("Public/Templates");
                    }
                    dc.UploadFile("Public\\Templates", fileName, fileData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Synchronize Templates in a users' Dropbox with the local Templates
        /// </summary>
        private void syncDropboxNow()
        {
            try
            {
                if (Properties.Settings.Default.dropboxEnabled)
                {
                    String dropboxTemplatePath = "\\Public\\Templates";

                    DropNetClient dc = new DropNetClient("2eie8xk4bqetn7m", "q4gjahtqu0w0le8");
                    DropNet.Models.UserLogin ul = new DropNet.Models.UserLogin();
                    
                    ul = dc.Login(Properties.Settings.Default.dropboxUsername, 
                                  Properties.Settings.Default.dropboxPassword);

                    if (ul.Secret == null || ul.Token == null)
                    {
                        MessageBox.Show("Invalid Dropbox credentials specified! Synchronisation could not be performed!", "Winstruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var folderDetails = dc.GetMetaData(dropboxTemplatePath);
                    var contents = folderDetails.Contents;
                    
                    if (contents.Count == 0)
                    {
                        dc.CreateFolder("Public/Templates");
                        return;
                    }

                    foreach (var file in contents)
                    {
                        String remoteFileName = file.Name;
                        String localFile = templatePath + remoteFileName;
                        if (File.Exists(localFile))
                        {
                            FileInfo fileInfo = new FileInfo(@localFile);
                            if (fileInfo.LastWriteTime.Ticks >= file.ModifiedDate.Ticks)
                            {
                                byte[] fileData = File.ReadAllBytes(localFile);
                                dc.UploadFile("Public\\Templates", file.Name, fileData);
                            }
                        }
                        else
                        {
                            byte[] fileData = dc.GetFile("Public\\Templates\\" + file.Name);
                            FileStream fs = new FileStream(localFile, FileMode.Create, FileAccess.ReadWrite);
                            BinaryWriter bw = new BinaryWriter(fs);
                            bw.Write(fileData);
                            bw.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}
