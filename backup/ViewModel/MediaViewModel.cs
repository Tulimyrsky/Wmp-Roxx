using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModel
{
    public class MediaViewModel : INotifyPropertyChanged
    {
        #region Fields

        private List<string> m_video;
        private string m_mediaFileName;
        private int m_index;

        #endregion

        #region Properties

        public List<string> Videos
        {
            get {return m_video;}
            set
            {
                if (m_video != value)
                {
                    m_video = value;
                    OnPropertyChanged("Videos");
                }
            }
        }

        public ActionCommand PlayCommand { get; private set; }
        public ActionCommand PauseCommand { get; private set; }

        public int Index
        {
            get { return m_index; }
            set
            {
                if (m_index != value)
                {
                    m_index = value;
                    OnPropertyChanged("Index");
                }
            }
        }

        public string MediaFilePath
        {
            get { return m_mediaFileName; }
            set
            {
                if (m_mediaFileName != value)
                {
                    m_mediaFileName = value;
                    OnPropertyChanged("MediaFilePath");
                }
            }
        }


        #endregion

        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler Play;
        public event EventHandler Pause;

        #endregion

        #region CTOR

        public MediaViewModel()
        {
            PlayCommand = new ActionCommand()
            {
                Action = () =>
                {
                    if (Index > -1)
                    {
                        MediaFilePath = MediaMgr.VideoMedias.ElementAt(Index).FileName;
                        if (Play != null)
                            Play.Invoke(this, EventArgs.Empty);
                    }
                }
            };

            PauseCommand = new ActionCommand()
            {
                Action = () =>
                {
                    if (Pause != null)
                        Pause.Invoke(this, EventArgs.Empty);
                }
            };

            m_video = new List<string>();

            Initialze();
        }

        #endregion

        #region Method


        public void Initialze()
        {
            MediaMgr.Initialize();

            Videos = (from el in MediaMgr.VideoMedias select el.Name).ToList();
        }


        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


    }
}
