using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Model
{
    public class MediaVideo : IMedia
    {
        char[] delim = { '/'};

        #region Properties

        #endregion

        #region CTOR

        public MediaVideo(string _file)
        {

            FileName = _file;
            Name = Path.GetFileNameWithoutExtension(_file);
        }

        #endregion
    }
}
