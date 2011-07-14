using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Winstruct
{
    [Serializable]
    public class BaseFile
    {
        enum FileMode 
        {
            CustomText = 0, 
            URL = 1
        }
        public string Name = "Untitled";
        public string Shortcut;
        public int Mode = (int) FileMode.CustomText;
        public string Content;

        public BaseFile()
        {
        }

        public void createFolder()
        {

        }
    }
}
