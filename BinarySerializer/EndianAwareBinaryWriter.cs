using System;
using System.Text;
using System.IO;

namespace BinarySerialization
{
    /// <summary>
    /// An extension of the <see cref="BinaryWriter"/> class that supports big- and little-endian byte ordering.
    /// </summary>
    public class EndianAwareBinaryWriter : BinaryWriter
    {
        private readonly IBitStream _output;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndianAwareBinaryWriter"/> class based on the specified
        /// stream and using UTF-8 encoding.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public EndianAwareBinaryWriter(IBitStream output): base(output.BaseStream)
        {
            _output = output;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndianAwareBinaryWriter"/> class based on the specified 
        /// stream and character encoding.
        /// </summary>
        /// <param name="output">The output stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public EndianAwareBinaryWriter(IBitStream output, Encoding encoding) : base(output.BaseStream, encoding)
        {
            _output = output;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndianAwareBinaryWriter"/> class based on the specified 
        /// stream, character encoding, and endianness.
        /// </summary>
        /// <param name="output">The output stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="endianness">The byte ordering to use.</param>
        public EndianAwareBinaryWriter(IBitStream output, Encoding encoding, Endianness endianness) : base(output.BaseStream, encoding)
        {
            _output = output;
            Endianness = endianness;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndianAwareBinaryWriter"/> class based on the specified 
        /// stream and endianness using UTF-8 encoding.
        /// </summary>
        /// <param name="output">The input stream.</param>
        /// <param name="endianness">The byte ordering to use.</param>
        public EndianAwareBinaryWriter(IBitStream output, Endianness endianness) : base(output.BaseStream)
        {
            _output = output;
            Endianness = endianness;
        }

        /// <summary>
        /// The byte ordering to use.
        /// </summary>
        public Endianness Endianness { get; set; }


        public override void Write(Int16 value)
        {
            var v = Endianness == Endianness.Big ? Bytes.Reverse(value) : value;
            base.Write(v);
        }

        public override void Write(UInt16 value)
        {
            var v = Endianness == Endianness.Big ? Bytes.Reverse(value) : value;
            base.Write(v);
        }
        
        public override void Write(Int32 value)
        {
            var v = Endianness == Endianness.Big ? Bytes.Reverse(value) : value;
            base.Write(v);
        }

        public override void Write(UInt32 value)
        {
            var v = Endianness == Endianness.Big ? Bytes.Reverse(value) : value;
            base.Write(v);
        }

        public override void Write(Int64 value)
        {
            var v = Endianness == Endianness.Big ? Bytes.Reverse(value) : value;
            base.Write(v);
        }

        public override void Write(UInt64 value)
        {
            var v = Endianness == Endianness.Big ? Bytes.Reverse(value) : value;
            base.Write(v);
        }

        public override void Write(float value)
        {
            var v = Endianness == Endianness.Big ? Bytes.Reverse(value) : value;
            base.Write(v);
        }

        public override void Write(double value)
        {
            var v = Endianness == Endianness.Big ? Bytes.Reverse(value) : value;
            base.Write(v);
        }

        public void Write(byte value, byte bitSize)
        {
            _output.WriteBits(value, bitSize);
        }
    }
}
