using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;

namespace ViewModel
{
    public class MediaViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<IMedia> m_media;
        private int m_where;
        private string m_mediaFileName;
        private int m_index;
        private TimeSpan _position;

        #endregion

        #region Properties

        public TimeSpan Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }

        public ObservableCollection<IMedia> Media
        {
            get { return m_media; }
            set
            {
                m_media = value;
                OnPropertyChanged("Media");
            }
        }

        public ActionCommand PlayCommand { get; private set; }
        public ActionCommand PauseCommand { get; private set; }
        public ActionCommand ListCommand { get; private set; }
        public ActionCommand FilterCommand { get; private set; }
        public ActionCommand SeekToMedia { get; private set; }

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
                    if (Enum.IsDefined(typeof(Model.Medias), m_where))
                        Media = MediaMgr.getList((Model.Medias)m_where);
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
        /*    ListCommand = new ActionCommand()
            {
                ActionP = (object index) =>
                  {
                      int value;

                      if (Int32.TryParse(index.ToString(), out value) && Enum.IsDefined(typeof(Model.Medias), value))
                      {
                          Application app = System.Windows.Application.Current;
                          if (app != null)
                              app.Dispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(Add), value);
                      }
                  }
            };*/

            SeekToMedia = new ActionCommand()
            {
                ActionP = (object media) =>
                    {
                        MediaElement element = media as MediaElement;

                        if (element.NaturalDuration.HasTimeSpan)
                            element.Position = Position;
                    }
            };


            PlayCommand = new ActionCommand()
            {
                Action = () =>
                {
                    if (Index > -1 && Index < Media.Count)
                    {
                        MediaFilePath = Media.ElementAt(Index).FileName;
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

            m_media = new ObservableCollection<IMedia>();
            Initialize();
        }

        private object Add(object value)
        {
            m_media = MediaMgr.getList((Medias)value);
            return null;
        }

        #endregion

        #region Method

        public void Initialize()
        {
            MediaMgr.Initialize();
            Media = MediaMgr.getList(Medias.VIDEO);
        }


        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


    }
}
