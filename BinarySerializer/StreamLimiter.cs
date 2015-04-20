using System;
using System.IO;

namespace BinarySerialization
{
    internal class StreamLimiter : IBitStream
    {
        private readonly bool _canSeek;
        private readonly long _length;
        private readonly IBitStream _sourceStream;
        private readonly long _maxLength;

        private long _position;
        private long _initialPosition;

        public StreamLimiter(IBitStream sourceStream, long maxLength = long.MaxValue)
        {
            if (sourceStream == null)
                throw new ArgumentNullException("sourceStream");

            if (maxLength < 0)
                throw new ArgumentOutOfRangeException("maxLength", "Cannot be negative.");


            _sourceStream = sourceStream;
            _maxLength = maxLength;

            /* Store for performance */
            _canSeek = sourceStream.CanSeek;

            if(_canSeek)
                _length = sourceStream.Length;

            _initialPosition = sourceStream.Position;
        }

        /// <summary>
        ///     The underlying source <see cref="Stream" />.
        /// </summary>
        public Stream BaseStream {
            get { return _sourceStream.BaseStream; } }

        public bool IsAtLimit
        {
            get { return Position >= MaxLength; }
        }

        public bool CanSeek
        {
            get { return _canSeek; }
        }

        public bool CanWrite
        {
            get { return false; }
        }

        public long MaxLength
        {
            get { return _maxLength; }
        }

        public long Length
        {
            get
            {
                if (!_canSeek)
                    return _sourceStream.Length;

                return _length;
            }
        }

        public long Remainder
        {
            get
            {
                if (!_canSeek)
                    return MaxLength - Position;

                return Math.Min(MaxLength, Length) - Position;
            }
        }

        public long Position
        {
            get
            {
                return _sourceStream.Position - _initialPosition;
            }
            set
            {
                _sourceStream.Position = value + _initialPosition;
            }
        }

        public void Flush()
        {
            _sourceStream.Flush();
        }

        public long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.End:
                    Position = Length + offset;
                    break;
            }

            return Position;
        }

        public void SetLength(long value)
        {
            throw new NotSupportedException();
        }

/*        public int Read(byte[] buffer, int offset, int count)
        {
            if (count > MaxLength - Position)
            {
                count = Math.Max(0, (int) (MaxLength - Position));
            }

            if (count == 0)
                return 0;

            int read = _sourceStream.Read(buffer, offset, count);
            _position += read;
            return read;
        }*/

        public void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public void WriteBits(byte value, byte bitSize)
        {
            throw new NotImplementedException();
        }

        public byte ReadBits(byte bitSize)
        {
            return _sourceStream.ReadBits(bitSize);
        }
    }
}