using System;
using System.Collections.Generic;
using Winstruct;
using Winstruct.DALTableAdapters;

namespace Winstruct
{
    class ContentTemplate
    {
        private ContentTemplatesTableAdapter ta;
        private DAL.ContentTemplatesDataTable templates;

        /// <summary>
        /// 
        /// </summary>
        public ContentTemplate()
        {
            this.ta = new ContentTemplatesTableAdapter();
            this.templates = ta.GetContentTemplates();
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
        public DAL.ContentTemplatesRow get(string name)
        {             
            this.templates = this.ta.GetContentTemplateByName(name);
            if (this.templates.Count > 0)
            {
                DAL.ContentTemplatesRow item = this.templates[0];
                return item;
            }

            DAL.ContentTemplatesRow row = null;
            return row;          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool add(string name, string shortcut, int contenttype, string content)
        {
            try
            {
                DAL.ContentTemplatesRow tplNew = this.templates.AddContentTemplatesRow(name, shortcut, contenttype + 1, content);
                int result = this.ta.Update(tplNew);
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
        public bool edit(string name, string shortcut, int contentType, string content, string oldName)
        {
            try
            {
                DAL.ContentTemplatesRow item = this.get(oldName);
                if (item != null)
                {
                    int result = ta.UpdateContentTemplate(name, shortcut, contentType + 1, content, oldName);
                    if (result > 0) return true;                    
                }
                else 
                {
                    if (this.add(name, shortcut, contentType, content))
                    {
                        return true;
                    }
                }

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
                int result = this.ta.DeleteContentTemplate(name);
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
