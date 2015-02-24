using System;

namespace BinarySerialization
{
    /// <summary>
    /// Identifies how many bits the specified field takes. Fields are populated from least significant bits
    /// to most significant bits.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false)]
    public sealed class BitSizeAttribute : Attribute
    {
        /// <summary>
        /// The number of bits the property will take during serialization and deserialization 
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldOrderAttribute"/> class.
        /// </summary>
        public BitSizeAttribute(int size)
        {
            Size = size;
        }
    }
}