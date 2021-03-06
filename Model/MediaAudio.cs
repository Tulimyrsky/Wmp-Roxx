﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Model
{
    public class MediaAudio : IMedia
    {


        #region Properties

        #endregion

        #region CTOR

        public MediaAudio(string _file)
        {
            FileName = _file;
            Name = Path.GetFileNameWithoutExtension(_file);
            using (FileStream fs = new FileStream(FileName, FileMode.Open))
            {
                try
                {
                    byte[] bytes = new byte[128];

                    fs.Seek(-128, SeekOrigin.End);
                    fs.Read(bytes, 0, 128);
                    if (System.Text.Encoding.Default.GetString(bytes, 0, 3).CompareTo("TAG") == 0)
                    {
                        Title = System.Text.Encoding.Default.GetString(bytes, 3, 30);
                        Artist = System.Text.Encoding.Default.GetString(bytes, 33, 30);
                        Album = System.Text.Encoding.Default.GetString(bytes, 63, 30);
                        Year = System.Text.Encoding.Default.GetString(bytes, 93, 4);
                        Comment = System.Text.Encoding.Default.GetString(bytes, 97, 30);
                    }
                }
                catch (Exception) { }
            }
        }

        #endregion
    }
}
