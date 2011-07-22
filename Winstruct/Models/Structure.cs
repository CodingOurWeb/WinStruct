using System;
using System.IO;
using System.Collections;
using System.Net;
using Ionic.Zip;
using Winstruct.DALTableAdapters;

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
        private DAL.ContentTemplatesDataTable table;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        public Structure(string[] txt = null)
        {
            this.Lines = txt;
            Errors = new ArrayList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Line"></param>
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
                        DAL.ContentTemplatesRow row = this._inDataTable(lineParts[1]);
                        this._createFolder(row.content, this.projectDir + lineParts[0]);
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
                        DAL.ContentTemplatesRow row = this._inDataTable(lineParts[1]);
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
                                if (row.contenttype == 1)
                                {
                                    this._createFile(this.projectDir + currentDir + lineParts[0], row.content);
                                }
                                else
                                {
                                    this._createFileFromUrl(this.projectDir + currentDir + lineParts[0], row.content);
                                }                               
                            }
                            else if (Last == -1) // We're dealing with a single file for project root
                            {
                                if (row.contenttype == 1)
                                {
                                    this._createFile(this.projectDir + lineParts[0], row.content);
                                }
                                else
                                {
                                    this._createFileFromUrl(this.projectDir + lineParts[0], row.content);
                                }                              
                            }
                            else // We're dealing with a folder/file combination
                            {
                                if (Last > 0)
                                {
                                    this._createFolder(this.projectDir + Line.Substring(0, Last));
                                    currentDir = Line.Substring(0, Last);
                                }
                                if (row.contenttype == 1)
                                {
                                    this._createFile(this.projectDir + lineParts[0], row.content);
                                }
                                else
                                {
                                    this._createFileFromUrl(this.projectDir + lineParts[0], row.content);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected Boolean _createProjectDir()
        {
            this.projectDir = this.basePath + "\\" + this.projectName + "\\";

            if (this._createFolder(this.projectDir))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Line"></param>
        /// <returns></returns>
        protected Boolean _isFolder(String Line)
        {
            char Last = Line[Line.Length - 1];
            if (Line.EndsWith("\\") || Line.StartsWith(":") || Line.IndexOf("\\:") > 0)
            {
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        protected Boolean _createFolder(String folderName)
        {
            FileSystemInfo fsi = Directory.CreateDirectory(folderName);
            if (fsi.Exists)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="folderName"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        private void _createFile(String fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            FileStream fstr = fi.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="Content"></param>
        protected void _createFile(string fileName, string Content)
        {
            File.WriteAllText(@fileName, Content);        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="Url"></param>
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

        /// <summary>
        /// 
        /// </summary>
        private void _setDataTable()
        {
            ContentTemplatesTableAdapter ta = new ContentTemplatesTableAdapter();
            table = ta.GetContentTemplates();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Shortcut"></param>
        /// <returns></returns>
        private DAL.ContentTemplatesRow _inDataTable(string Shortcut)
        {
            foreach (DAL.ContentTemplatesRow row in table.Rows)
            {
                if (row.shortcut == Shortcut)
                {
                    return row;
                }
            }

            return null;
        }

#endregion

    }
}
