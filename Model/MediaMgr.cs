using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;

namespace Model
{
    public static class MediaMgr
    {
        #region Properties

        private static ObservableCollection<IMedia> VideoMedias { get; set; }
        private static ObservableCollection<IMedia> AudioMedias { get; set; }
        private static ObservableCollection<IMedia> ImageMedias { get; set; }
        private static Dictionary<Medias, ObservableCollection<IMedia>> all;


        #endregion

        #region Methods

        public static void Initialize()
        {
            VideoMedias = new ObservableCollection<IMedia>();
            AudioMedias = new ObservableCollection<IMedia>();
            ImageMedias = new ObservableCollection<IMedia>();

            loadFolder(Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures));
            loadFolder(Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic));
            loadFolder(Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos));
            loadFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            loadFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            loadFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

            all = new Dictionary<Medias, ObservableCollection<IMedia>>();
            all.Add(Medias.VIDEO, VideoMedias);
            all.Add(Medias.AUDIO, AudioMedias);
            all.Add(Medias.IMAGE, ImageMedias);
        }

        public static void loadFolder(string folder)
        {
            try
            {
                foreach (string file in Directory.GetFiles(folder, "*", SearchOption.AllDirectories))
                    loadMedia(file);
            }
            catch (Exception e)
            {
                Console.WriteLine("=============== ACCESS FOLDER " + folder + " ERROR: "  + e.Message);
            }
        }

        public static void loadMedia(string file)
        {
            char[] delim = { '/'};
            String extension = Path.GetExtension(file);
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);
            if (key.GetValue("Content Type") != null)
            {
                switch (key.GetValue("Content Type").ToString().Split(delim)[0])
                {
                    case "video":
                        VideoMedias.Add(new MediaVideo(file));
                        break;
                    case "audio":
                        AudioMedias.Add(new MediaAudio(file));
                        break;
                    case "image":
                        ImageMedias.Add(new MediaImage(file));
                        break;
                }
            }
        }

        public static ObservableCollection<IMedia> getList(Medias index)
        {
            return all[index];
        }

        #endregion
    }
}
