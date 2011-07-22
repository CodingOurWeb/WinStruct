using System;
using System.Collections.Generic;
using Winstruct;
using Winstruct.DALTableAdapters;

namespace Winstruct
{
    class ProjectTemplate
    {
        private ProjectTemplatesTableAdapter ta;
        private DAL.ProjectTemplatesDataTable templates;

        public ProjectTemplate()
        {
            this.ta = new DALTableAdapters.ProjectTemplatesTableAdapter();
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
                DAL.ProjectTemplatesDataTable template = ta.GetTemplateByID(id);
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
                DAL.ProjectTemplatesDataTable template = ta.GetTemplateByName(name);
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
                DAL.ProjectTemplatesRow tplNew = templates.AddProjectTemplatesRow(name, content);
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
