using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winstruct;

namespace Winstruct
{
    class ProjectTemplate
    {
        private ProjectTemplatesTableAdapters.ProjectTemplatesTableAdapter ta;
        private ProjectTemplates.ProjectTemplatesDataTable templates;

        public ProjectTemplate()
        {
            this.ta = new ProjectTemplatesTableAdapters.ProjectTemplatesTableAdapter();
            this.templates = ta.GetTemplates();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> get()
        {
            Dictionary<int, string> items = new Dictionary<int, string>();
            
            try
            {
                for (int i = 0; i < this.templates.Count; i++)
                {
                    items.Add(this.templates[i].id, this.templates[i].name);
                }
                return items;
            }
            catch (Exception)
            {
                return items;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string get(int id)
        {
            try
            {
                ProjectTemplates.ProjectTemplatesDataTable template = ta.GetTemplateByID(id);
                return template[0].content;
            }
            catch (Exception)
            {
                return "";
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string get(string name)
        {
            try
            {
                Templates.WinstructTemplatesDataTable template = ta.GetTemplateByName(name);
                return template[0].content;
            }
            catch (Exception)
            {
                return "";
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool add(string name, string content)
        {
            try
            {
                Templates.WinstructTemplatesRow tplNew = templates.AddWinstructTemplatesRow(name, content);
                int result = ta.Update(tplNew);
                if (result > 0) return true;
                return false;                
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool edit(string name, string content)
        {
            try
            {

                int result = ta.UpdateTemplate(name, content);
                if (result > 0) return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool delete(string name)
        {
            try
            {
                int result = this.ta.DeleteTemplate(name);
                if (result > 0) return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
