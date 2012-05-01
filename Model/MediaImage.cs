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
            FileName = _file;
            Name = Path.GetFileNameWithoutExtension(FileName);
        }

        #endregion
    }
}
