using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Xml;
using System.Net;
using Ionic.Zip;

namespace Winstruct
{
    public class Structure
    {
        /// <summary>
        /// index.php           -> file (no \ present)
        /// index.php:html      -> template (no \ present and : present)
        /// css\                -> folder (ends with \)
        /// \style.css          -> file (starts with \)
        /// \style.css:reset    -> template (starts with \ and : present)
        /// :reset              -> template (starts with :)
        /// css\style.css       -> file (has one or more \ [not at start or end])
        /// css\reset.css:reset -> template (has one or more \ [not at start or end] and : present)
        /// 
        /// wordpress\          -> create folder named wordpress
        /// :wp                 -> download WP installation to wordpress folder
        /// 
        /// :ci                 -> download CI installation to root of project folder
        /// </summary>

        public string basePath;
        public string projectName;
        public string templatePath;

        private string projectDir;
        private string currentDir;

        private string[] Lines;

        private ArrayList Errors;
        private DataTable table;

        public Structure(string[] txt = null)
        {
            this.Lines = txt;
            Errors = new ArrayList();
        }

        public ArrayList Parse()
        {
            if (this._createProjectDir())
            {
                this._setDataTable();

                foreach (String Line in this.Lines)
                {
                    this._processLine(Line);
                }
            }
            else
            {
                Errors.Add("Could not create project directory");
            }

            return Errors;
        }


#region Helpers

        protected void _processLine(String Line)
        {
            if (string.IsNullOrEmpty(Line) != true)
            {
                string[] lineParts = Line.Split(':');

                if (this._isFolder(Line))
                {
                    currentDir = Line;

                    if (lineParts.Length > 1)
                    {
                        DataRow row = this._inDataTable(lineParts[1]);
                        this._createFolder((string)row["Content"], this.projectDir + lineParts[0]);
                    }
                    else
                    {
                        this._createFolder(this.projectDir + Line);
                    }
                }
                else // Ok, so we're dealing with a file!
                {
                    
                    // ...and there is a colon present
                    if (lineParts.Length > 1)
                    {
                        DataRow row = this._inDataTable(lineParts[1]);
                        if (row == null)
                        {
                            Errors.Add("Could not process line: " + Line);
                            return;
                        } 

                        int Last = Line.LastIndexOf('\\');
                        if (row.ItemArray.Length > 0)
                        {
                            // We're dealing with a single file for current dir
                            if (Line.StartsWith("\\"))
                            {
                                if ((int)row["Mode"] == 0)
                                {
                                    this._createFile(this.projectDir + currentDir + lineParts[0], (string)row["Content"]);
                                }
                                else
                                {
                                    this._createFileFromUrl(this.projectDir + currentDir + lineParts[0], (string)row["Content"]);
                                }                               
                            }
                            else if (Last == -1) // We're dealing with a single file for project root
                            {
                                if ((int)row["Mode"] == 0)
                                {
                                    this._createFile(this.projectDir + lineParts[0], (string)row["Content"]);
                                }
                                else
                                {
                                    this._createFileFromUrl(this.projectDir + lineParts[0], (string)row["Content"]);
                                }                              
                            }
                            else // We're dealing with a folder/file combination
                            {
                                if (Last > 0)
                                {
                                    this._createFolder(this.projectDir + Line.Substring(0, Last));
                                    currentDir = Line.Substring(0, Last);
                                }
                                if ((int)row["Mode"] == 0)
                                {
                                    this._createFile(this.projectDir + lineParts[0], (string)row["Content"]);
                                }
                                else
                                {
                                    this._createFileFromUrl(this.projectDir + lineParts[0], (string)row["Content"]);
                                } 
                            }
                        }
                    }
                    else // There is no colon
                    {
                        int Last = Line.LastIndexOf('\\');

                        // We're dealing with a single file for current dir
                        if (Line.StartsWith("\\"))
                        {
                            this._createFile(this.projectDir + currentDir + Line);
                        }
                        else if (Last == -1) // We're dealing with a single file for project root
                        {
                            this._createFile(this.projectDir + Line);
                        }
                        else // We're dealing with a folder/file combination
                        {
                            if (Last > 0)
                            {
                                this._createFolder(this.projectDir + Line.Substring(0, Last));
                                currentDir = Line.Substring(0, Last);
                            }
                            this._createFile(this.projectDir + Line);
                        }
                    }
                }
            } 
        }

        protected Boolean _createProjectDir()
        {
            this.projectDir = this.basePath + "\\" + this.projectName + "\\";

            if (this._createFolder(this.projectDir))
            {
                return true;
            }
            return false;
        }

        protected Boolean _isFolder(String Line)
        {
            char Last = Line[Line.Length - 1];
            if (Line.EndsWith("\\") || Line.StartsWith(":") || Line.IndexOf("\\:") > 0)
            {
                return true;
            }

            return false;
        }

        protected Boolean _createFolder(String folderName)
        {
            FileSystemInfo fsi = Directory.CreateDirectory(folderName);
            if (fsi.Exists)
            {
                return true;
            }

            return false;
        }

        protected void _createFolder(String Url, String folderName)
        {
            if (! Directory.Exists(folderName))
            {
                this._createFolder(folderName);
            }
            String zipFile = folderName + "tmp.zip";
            
            WebClient Client = new WebClient();
            Client.DownloadFile(Url, @zipFile);

            String rootFolder = "";
            bool replaceFiles = false;

            using (ZipFile zip = ZipFile.Read(zipFile)) { 
                if (zip[0].IsDirectory)
                {
                    rootFolder = zip[0].FileName;
                    replaceFiles = true;
                }
                foreach (ZipEntry entry in zip) {
                    if (entry.FileName.StartsWith(rootFolder) == false)
                    {
                        replaceFiles = false;
                    }
		            entry.Extract(folderName, ExtractExistingFileAction.OverwriteSilently);
	            }
            }

            File.Delete(zipFile);

            if (replaceFiles)
            {
                DirectoryInfo source = new DirectoryInfo(folderName + "\\" + rootFolder);
                DirectoryInfo target = new DirectoryInfo(folderName);
                CopyFilesRecursively(source, target);
            
                Directory.Delete(folderName + "\\" + rootFolder, true);
            }

        }

        private void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }

        private void _createFile(String fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            FileStream fstr = fi.Create();
        }

        protected void _createFile(string fileName, string Content)
        {
            File.WriteAllText(@fileName, Content);        
        }

        protected void _createFileFromUrl(string fileName, string Url)
        {
            int lastBackslash = fileName.LastIndexOf("\\");
            string filePath = fileName.Substring(0, lastBackslash);
            if (!Directory.Exists(filePath))
            {
                this._createFolder(filePath);
            }

            WebClient Client = new WebClient();
            Client.DownloadFile(Url, @fileName);
        }

        private void _setDataTable()
        {
            table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Shortcut", typeof(string));
            table.Columns.Add("Mode", typeof(int));
            table.Columns.Add("Content", typeof(string));

            DirectoryInfo dir = new DirectoryInfo(this.templatePath);
            foreach (FileInfo fi in dir.GetFiles("*.xml"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fi.FullName);
                XmlNode nodeName = doc.SelectSingleNode("BaseFile/Name");
                XmlNode nodeShortcut = doc.SelectSingleNode("BaseFile/Shortcut");
                XmlNode nodeMode = doc.SelectSingleNode("BaseFile/Mode");
                XmlNode nodeContent = doc.SelectSingleNode("BaseFile/Content");

                table.Rows.Add(nodeName.InnerText, nodeShortcut.InnerText, nodeMode.InnerText, nodeContent.InnerText);
            }
        }

        private DataRow _inDataTable(string Shortcut)
        {
            foreach (DataRow row in table.Rows)
            {
                if ((string)row.ItemArray[1] == Shortcut)
                {
                    return row;
                }
            }

            return null;
        }

#endregion

    }
}
