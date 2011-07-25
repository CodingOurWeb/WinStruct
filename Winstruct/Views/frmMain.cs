using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace Winstruct
{
    public partial class frmMain : Form
    {
        private bool slashEntered = false;

        private Structure structure;
        private Form progress;

        /// <summary>
        /// Initialize form and components
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            Option opt = new Option();
            txtBasePath.Text = opt.getValue("basePath"); //Properties.Settings.Default.basePath;
            txtProjectName.Text = opt.getValue("defaultProjectName"); //Properties.Settings.Default.defaultProjectName;

            FillTemplatesCombo();
        }

        /// <summary>
        /// Get all templates from database and fill the combo
        /// </summary>
        private void FillTemplatesCombo()
        {
            ProjectTemplate tpl = new ProjectTemplate();

            txtTemplateName.Clear();
            cboTemplates.Items.Clear();
            cboTemplates.Items.Add("Custom");
            
            Dictionary<int, string> items = tpl.get();
            foreach (KeyValuePair<int, string> item in items)
            {
                cboTemplates.Items.Add(item.Value);
            }

            cboTemplates.SelectedIndex = 0;
        }

        /// <summary>
        /// Browse for the output folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                DialogResult result = folder.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Option opt = new Option();
                    if (opt.setValue("basePath", folder.SelectedPath + "\\"))
                    {
                        txtBasePath.Text = folder.SelectedPath + "\\";
                    }
                }
                folder.Dispose();
                folder = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Create a new thread for creating our structure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            progress = new frmProgress();
            progress.Show();
            //progress.StartPosition = FormStartPosition.CenterParent;

            Thread t = new Thread(new ThreadStart(CreateStructure));
            t.Start();
        }

        /// <summary>
        /// Start creating our project file structure on the newly created thread
        /// </summary>
        private void CreateStructure()
        {
            try
            {
                structure = new Structure(txtStructure.Lines);
                structure.basePath = txtBasePath.Text;
                structure.projectName = txtProjectName.Text;

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

        /// <summary>
        /// When finished creating structure
        /// </summary>
        private void OnComplete()
        {
            progress.Close();
            progress.Dispose();
        }
        private delegate void Complete();

        /// <summary>
        /// Save the current listing as a template
        /// 1. Template specified and Template name specified -> rename
        /// 2. Template specified and Template name empty -> update
        /// 3. Template is Custom and Template name specified -> create
        /// </summary>
        private void SaveTemplate()
        {
            if (txtTemplateName.Text.Length == 0)
            {
                MessageBox.Show("Please provide a name for this template!", "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTemplateName.Focus();
                return;
            }

            if (txtStructure.Text.Length == 0)
            {
                MessageBox.Show("Please provide some content for this template!", "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStructure.Focus();
                return;
            }

            try
            {
                ProjectTemplate tpl = new ProjectTemplate();
                string oldTemplate = cboTemplates.Text;
                if (cboTemplates.SelectedIndex > 0 && txtTemplateName.Text.Length > 0 && tpl.add(txtTemplateName.Text, txtStructure.Text))
                {
                    MessageBox.Show("Template '" + txtTemplateName.Text + "' was successfully renamed.", "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillTemplatesCombo();
                    cboTemplates.SelectedItem = txtTemplateName.Text;
                }
                else if (cboTemplates.Text.Length > 0 && txtTemplateName.Text.Length == 0 && tpl.edit(cboTemplates.Text, txtStructure.Text))
                {
                    MessageBox.Show("Template '" + cboTemplates.Text + "' was successfully updated.", "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillTemplatesCombo();
                    cboTemplates.SelectedItem = oldTemplate;
                }
                else if (cboTemplates.SelectedIndex == 0 && txtTemplateName.Text.Length > 0 && tpl.add(txtTemplateName.Text, txtStructure.Text))
                {
                    MessageBox.Show("Template '" + txtTemplateName.Text + "' was successfully created.", "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillTemplatesCombo();
                    cboTemplates.SelectedItem = txtTemplateName.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating template '" + txtTemplateName.Text + "': " + ex.Message, "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Events
        
            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void txtStructure_KeyDown(object sender, KeyEventArgs e)
            {
                slashEntered = false;
                if (e.KeyValue == 191)
                {
                    slashEntered = true;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void txtStructure_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (slashEntered == true)
                {
                    SendKeys.Send("\\");
                    e.Handled = true;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void btnContentManager_Click(object sender, EventArgs e)
            {
                Form cm = new frmContentManager();
                cm.ShowDialog();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void clearStructure(object sender, EventArgs e)
            {
                txtStructure.Clear();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void exitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void cboTemplates_SelectedIndexChanged(object sender, EventArgs e)
            {
                txtTemplateName.Clear();

                ProjectTemplate tpl = new ProjectTemplate();
                txtStructure.Text = tpl.get(cboTemplates.Text);

                if (cboTemplates.SelectedIndex > 0 || txtStructure.Text.Length > 0)
                {
                    btnDeleteTemplate.Enabled = true;
                    btnCreate.Enabled = true;
                    mnuCreateProject.Enabled = true;
                }
                else
                {
                    btnDeleteTemplate.Enabled = false;
                    btnCreate.Enabled = false;
                    mnuCreateProject.Enabled = false;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void SaveAsTemplate_Click(object sender, EventArgs e)
            {
                SaveTemplate();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void btnSaveAsTemplate_Click(object sender, EventArgs e)
            {
                SaveTemplate();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void btnDeleteTemplate_Click(object sender, EventArgs e)
            {
                try
                {
                    ProjectTemplate tpl = new ProjectTemplate();
                    if (tpl.delete(cboTemplates.SelectedItem.ToString()))
                    {
                        FillTemplatesCombo();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting template '" + txtTemplateName.Text + "': " + ex.Message, "WinStruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void txtStructure_TextChanged(object sender, EventArgs e)
            {
                if (txtStructure.Text.Length > 0)
                {
                    btnCreate.Enabled = true;
                }
                else
                {
                    btnCreate.Enabled = false;
                }
            }

        #endregion

    }
}
