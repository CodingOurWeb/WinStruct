using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Winstruct
{
    public partial class frmContentManager : Form
    {
        private BaseFile bf;
        private int curListItemIndex;
        private String templatePath;

        public frmContentManager()
        {
            InitializeComponent();

            templatePath = Application.StartupPath + "\\Templates\\";
            populateListBox();
        }

        private void btnFileAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a new BaseFile object
                bf = new BaseFile();

                curListItemIndex = lstFiles.Items.Add(bf.Name);
                lstFiles.SelectedIndex = curListItemIndex;
            
                txtName.Text = bf.Name;
                cboMode.SelectedIndex = bf.Mode;
                txtShortcut.Text = "";
                txtContent.Text = "";
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void btnFileSave_Click(object sender, EventArgs e)
        {
            saveToXml();
        }

        private void btnFileDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = lstFiles.SelectedIndex;
                string selectedText = (string)lstFiles.SelectedItem;

                lstFiles.Items.RemoveAt(selectedIndex);
                File.Delete(templatePath + "\\" + selectedText + ".xml");
                if (lstFiles.Items.Count > 0)
                {
                    lstFiles.SelectedIndex = 0;
                } 
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            bf.Name = txtName.Text;
            lstFiles.Items[curListItemIndex] = bf.Name;
        }

        private void populateListBox()
        {
            try
            {
                lstFiles.Items.Clear();

                DirectoryInfo dir = new DirectoryInfo(templatePath);
                foreach (FileInfo fi in dir.GetFiles("*.xml"))
                {
                    string result = Path.GetFileNameWithoutExtension(fi.FullName);
                    lstFiles.Items.Add(result);
                }

                if (lstFiles.Items.Count > 0)
                {
                    lstFiles.SelectedIndex = 0;
                    loadObjectFromXml(lstFiles.SelectedItem + ".xml");
                }
            }
            catch (Exception)
            {
                throw;   
            }
        }

        private void loadObjectFromXml(string fileName)
        {
            try
            {
                // Start deserialization
                XmlSerializer serializer = new XmlSerializer(typeof(BaseFile));
                FileStream fileStream = new FileStream(templatePath + "\\" + fileName, FileMode.Open);
                bf = (BaseFile)serializer.Deserialize(fileStream);
                fileStream.Close();
                fileStream.Dispose();
                fileStream = null;

                // Populate textboxes from Object
                txtName.Text = bf.Name;
                txtShortcut.Text = bf.Shortcut;
                cboMode.SelectedIndex = bf.Mode;
                txtContent.Text = bf.Content;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void saveToXml()
        {
            try
            {
                bf.Name = txtName.Text;
                bf.Shortcut = txtShortcut.Text;
                bf.Mode = cboMode.SelectedIndex;
                bf.Content = txtContent.Text;

                XmlSerializer serializer = new XmlSerializer(typeof(BaseFile));
                TextWriter textWriter = new StreamWriter(@templatePath + "\\" + bf.Name + ".xml");
                serializer.Serialize(textWriter, bf);
                textWriter.Close();
                textWriter.Dispose();
                textWriter = null;
            }
            catch (Exception)
            {                
                throw;           
            }
        }

        private void lstFiles_Click(object sender, EventArgs e)
        {
            loadObjectFromXml((string)lstFiles.SelectedItem + ".xml");
        }


    }
}
