using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Model
{
    public enum Medias
    {
        VIDEO,
        AUDIO,
        IMAGE
    };

    public abstract class IMedia : ISerializable, INotifyPropertyChanged
    {
        #region properties

        private string fileName;
        private string name;
        private string title;
        private string artist;
        private string album;
        private string year;
        private string comm;
        private Medias type;

        #endregion

        #region accessors
        public string FileName
        {
            get
            {
                return fileName;
            }
            protected set
            {
                if (fileName != value)
                {
                    fileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            protected set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Title
        {
            get { return title; }
            protected set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        public string Artist
        {
            get { return artist; }
            protected set
            {
                if (artist != value)
                {
                    artist = value;
                    OnPropertyChanged("Artist");
                }
            }
        }
        public string Album 
        {
            get { return album; }
            protected set
            {
                if (album != value)
                {
                    album = value;
                    OnPropertyChanged("Album");
                }
            }   
        }
        public string Year
        {
            get { return year; }
            protected set
            {
                if (year != value)
                {
                    year = value;
                    OnPropertyChanged("Year");
                }
            }   
        }
        public string Comment
        {
            get { return comm; }
            protected set
            {
                if (comm != value)
                {
                    comm = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
        public Medias Type
        {
            get { return type; }
            protected set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        #endregion

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }
}
