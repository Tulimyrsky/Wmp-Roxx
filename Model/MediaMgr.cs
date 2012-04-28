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

        public static ReadOnlyCollection<MediaVideo> VideoMedias { get; private set; }
        public static ReadOnlyCollection<MediaAudio> AudioMedias { get; private set; }
        public static ReadOnlyCollection<MediaImage> ImageMedias { get; private set; }

        #endregion

        #region Methods

        public static void Initialize()
        {

            string videoFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

            VideoMedias = (from el in Directory.GetFiles(videoFolder, "*", SearchOption.AllDirectories)
                        select new MediaVideo(el)).ToList().AsReadOnly();
            AudioMedias = (from el in Directory.GetFiles(videoFolder, "*", SearchOption.AllDirectories)
                        select new MediaAudio(el)).ToList().AsReadOnly();
            ImageMedias = (from el in Directory.GetFiles(videoFolder, "*", SearchOption.AllDirectories)
                        select new MediaImage(el)).ToList().AsReadOnly();
        }

        #endregion
    }
}
