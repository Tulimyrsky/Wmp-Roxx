using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Model
{
    public class MediaAudio
    {
        char[] delim = { '/'};

        #region Properties

        public string FileName  { get; private set; }
        public string Name      { get; private set; }
        public string Title     { get; private set; }
        public string Singer    { get; private set; }
        public string Album     { get; private set; }
        public string Year      { get; private set; }
        public string Comm      { get; private set; }

        #endregion

        #region CTOR

        public MediaAudio(string _file)
        {
            String extension = Path.GetExtension(_file);
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);
            if (key.GetValue("Content Type") != null && key.GetValue("Content Type").ToString().Split(delim)[0] == "audio")
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
                            Singer = System.Text.Encoding.Default.GetString(bytes, 33, 30);
                            Album = System.Text.Encoding.Default.GetString(bytes, 63, 30);
                            Year = System.Text.Encoding.Default.GetString(bytes, 93, 4);
                            Comm = System.Text.Encoding.Default.GetString(bytes, 97, 30);

                            System.Console.WriteLine("=======================");
                            System.Console.WriteLine("Title: " + Title);
                            System.Console.WriteLine("Singer: " + Singer);
                            System.Console.WriteLine("Album: " + Album);
                            System.Console.WriteLine("Year: " + Year);
                            System.Console.WriteLine("Comment: " + Comm);
                            System.Console.WriteLine("=======================");
                        }
                    }
                    catch (Exception) { }
                }
            }                 
        }

        #endregion
    }
}
