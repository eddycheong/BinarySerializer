using System.IO;

namespace BinarySerialization
{
    public class BitStream : Stream, IBitStream
    {
        private readonly Stream _source;
        private byte _readBitPosition;
        private byte? _stagedReadByte;
        private byte? _stagedWriteByte;
        private byte _writeBitPosition;

        public BitStream(Stream source)
        {
            _source = source;
        }

        public override bool CanRead
        {
            get { return _source.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _source.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _source.CanWrite; }
        }

        public override long Length
        {
            get { return _source.Length; }
        }

        public override long Position
        {
            get
            {
                if (_stagedReadByte.HasValue)
                {
                    return _source.Position - 1;
                }
                return _source.Position;
            }
            set
            {
                _source.Position = value;
            }
        }

        public override void Flush()
        {
            FlushStagedWriteBits();
            _source.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ResetReadBitPosition();
            return _source.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            FlushStagedWriteBits();
            return _source.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _source.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            FlushStagedWriteBits();
            _source.Write(buffer, offset, count);
        }

        public void WriteBits(byte value, byte bitSize)
        {
            if (bitSize <= 0)
            {
                WriteByte(value);
                return;
            }

            _stagedWriteByte = _stagedWriteByte ?? 0x00;
            var shiftedValue = (byte) (value << _writeBitPosition);
            _stagedWriteByte |= shiftedValue;
            _writeBitPosition += bitSize;
            if (_writeBitPosition >= 8)
            {
                FlushStagedWriteBits();
            }
        }

        private void FlushStagedWriteBits()
        {
            if (!_stagedWriteByte.HasValue) return;
            byte valueToFlush = _stagedWriteByte.Value;
            _stagedWriteByte = null;
            _writeBitPosition = 0;
            WriteByte(valueToFlush);
        }


        public byte ReadBits(byte bitSize)
        {
            if (bitSize <= 0)
            {
                return (byte)ReadByte();
            }

            if (!_stagedReadByte.HasValue)
            {
                _stagedReadByte = (byte) ReadByte();
            }
            var value = (byte) (_stagedReadByte >> _readBitPosition);
            var mask = (byte) ((1 << bitSize) - 1);
            value &= mask;

            _readBitPosition += bitSize;
            if (_readBitPosition >= 8)
            {
                ResetReadBitPosition();
            }

            return value;
        }

        public Stream BaseStream
        {
            get { return _source; }
        }

        private void ResetReadBitPosition()
        {
            _readBitPosition = 0;
            _stagedReadByte = null;
        }
    }
}