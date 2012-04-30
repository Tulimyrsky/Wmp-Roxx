using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Model
{
    public class MediaImage : IMedia
    {
        char[] delim = { '/'};

        #region Properties

        #endregion

        #region CTOR

        public MediaImage(string _file)
        {
            String extension = Path.GetExtension(_file);
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);
            if (key.GetValue("Content Type") != null && key.GetValue("Content Type").ToString().Split(delim)[0] == "image")
            {
                FileName = _file;
                Name = Path.GetFileNameWithoutExtension(FileName);
            }
 
        }

        #endregion
    }
}
