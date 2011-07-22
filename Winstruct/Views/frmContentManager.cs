using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Winstruct
{
    public partial class frmContentManager : Form
    {
        private int curListItemIndex;
        private string oldName = "";
        private bool bAdding = false;

        /// <summary>
        /// 
        /// </summary>
        public frmContentManager()
        {
            InitializeComponent();

            populateListBox();
        }
    
        /// <summary>
        /// 
        /// </summary>
        private void populateListBox()
        {
            try
            {
                lstContentTemplates.Items.Clear();
                ContentTemplate ctpl = new ContentTemplate();
                Dictionary<int, string> items = ctpl.get();
                foreach (KeyValuePair<int, string> item in items)
                {
                    lstContentTemplates.Items.Add(item.Value);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Winstruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileAdd_Click(object sender, EventArgs e)
        {
            try
            {
                curListItemIndex = lstContentTemplates.Items.Add("Unnamed");
                lstContentTemplates.SelectedIndex = curListItemIndex;

                txtName.Text = "Unnamed";
                cboMode.SelectedIndex = 0;
                txtShortcut.Text = "";
                txtContent.Text = "";

                bAdding = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Winstruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileSave_Click(object sender, EventArgs e)
        {
            ContentTemplate ctpl = new ContentTemplate();

            if (ctpl.edit(txtName.Text, txtShortcut.Text, cboMode.SelectedIndex, txtContent.Text, oldName.Length == 0 ? txtName.Text : oldName))
            {
                ctpl.get();
                populateListBox();
            }
            else
            {
                MessageBox.Show("Content template could not be saved! Possibly name or shortcode are already in use?", "Winstruct", MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }

            bAdding = false;
            oldName = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = lstContentTemplates.SelectedIndex;
                string selectedText = (string)lstContentTemplates.SelectedItem;

                ContentTemplate ctpl = new ContentTemplate();

                if (ctpl.delete(selectedText))
                {
                    populateListBox();
                }
                else
                {
                    MessageBox.Show("Content template could not be deleted!", "Winstruct", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (bAdding) 
                lstContentTemplates.Items[curListItemIndex] = txtName.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstContentTemplates_Click(object sender, EventArgs e)
        {
            //curListItemIndex = lstContentTemplates.SelectedIndex;
            string selectedText = (string)lstContentTemplates.SelectedItem;

            if (selectedText.Length > 0 && selectedText != "Unnamed")
            {
                ContentTemplate ctpl = new ContentTemplate();
                DAL.ContentTemplatesRow item = ctpl.get(selectedText);

                if (item != null)
                {
                    txtName.Text = item.name;
                    txtShortcut.Text = item.shortcut;
                    cboMode.SelectedIndex = item.contenttype - 1;
                    txtContent.Text = item.content;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Click(object sender, EventArgs e)
        {
            if (! bAdding && oldName.Length == 0)
            {
                oldName = txtName.Text;
            }
        }

        #endregion

    }
}
