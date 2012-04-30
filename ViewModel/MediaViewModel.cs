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

        private List<IMedia> m_media;
        private int m_where;
        private string m_mediaFileName;
        private int m_index;

        #endregion

        #region Properties

        public List<IMedia> Media
        {
            get
            { return m_media; }
            set
            {
                if (m_media != value)
                {

                    m_media = value;
                    OnPropertyChanged("Media");
                }
            }
        }

        public ActionCommand PlayCommand { get; private set; }
        public ActionCommand PauseCommand { get; private set; }
        public ActionCommand ListCommand { get; private set; }

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

        public int TabIndex
        {
            get { return m_where; }
            set
            {
                if (m_where != value)
                {
                    m_where = value;
                    OnPropertyChanged("TabIndex");
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
            ListCommand = new ActionCommand()
            {
                ActionP = (object index) =>
                  {
                      switch ((int)index)
                      {
                          case 0 :
                              m_media = MediaMgr.VideoMedias.ToList<IMedia>();
                              break;
                          case 1 :
                              m_media = MediaMgr.AudioMedias.ToList<IMedia>();
                              break;
                          case 2 :
                              m_media = MediaMgr.ImageMedias.ToList<IMedia>();
                              break;
                          default :
                              Console.WriteLine("++++++++++++++++++++++++Mega prout tu prends cher babtard +++++ " + index.ToString());
                              break;
                      }
                      
                  }
            };

            PlayCommand = new ActionCommand()
            {
                Action = () =>
                {
                    if (Index > -1)
                    {
                        if (m_where == 0)
                            MediaFilePath = MediaMgr.VideoMedias.ElementAt(Index).FileName;
                        else if (m_where == 1)
                            MediaFilePath = MediaMgr.AudioMedias.ElementAt(Index).FileName;
                        else
                            MediaFilePath = MediaMgr.ImageMedias.ElementAt(Index).FileName;

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

            m_media = new List<IMedia>();

            Initialze();
        }

        #endregion

        #region Method


        public void Initialze()
        {
            MediaMgr.Initialize();

            Media = MediaMgr.VideoMedias.ToList<IMedia>();
        }


        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


    }
}
