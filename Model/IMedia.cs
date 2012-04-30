using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Model
{
    public abstract class IMedia : ISerializable
    {
        #region properties

        public string FileName { get; protected set; }
        public string Name { get; protected set; }
        public string Title { get; protected set; }
        public string Artist { get; protected set; }
        public string Album { get; protected set; }
        public string Year { get; protected set; }
        public string Comm { get; protected set; }

        #endregion

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
