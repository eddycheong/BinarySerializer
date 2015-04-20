using System;
using System.IO;

namespace BinarySerialization
{
    public interface IBitStream
    {
        bool CanSeek { get; }
        long Length { get; }
        long Position { get; set; }
        void Flush();
        long Seek(long offset, SeekOrigin origin);
        void WriteBits(byte value, byte bitSize);
        byte ReadBits(byte bitSize);
        Stream BaseStream { get; }
    }
}