using Winstruct;
using Winstruct.DALTableAdapters;
using System;

namespace Winstruct
{
    class Option
    {
        private OptionsTableAdapter ta;
        private DAL.OptionsDataTable options;

        public Option()
        {
            this.ta = new OptionsTableAdapter();
            this.options = ta.GetOptions();
        }

        public string getValue(string key)
        {
            return this.options.FindBykey(key).value;
        }

        public bool setValue(string key, string value)
        {
            try
            {
                int result = ta.UpdateOption(value, key);
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
