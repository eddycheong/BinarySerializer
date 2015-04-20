using System.IO;

namespace BinarySerialization
{
    /// <summary>
    /// Used to wrap streams to allow for relative position tracking whether the source stream is seekable or not.
    /// </summary>
    internal class StreamKeeper 
    {
        private readonly IBitStream _stream;
        private long _basePosition;

        public long RelativePosition
        {
            get
            {
                return _stream.Position - _basePosition;
            }
            set
            {
                _basePosition = _stream.Position - value;
            }
        }

        public StreamKeeper(IBitStream stream) 
        {
            _stream = stream;
        }


        public IBitStream Stream
        {
            get { return _stream; }
        }
    }
}
