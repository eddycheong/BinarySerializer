using BinarySerialization;

namespace BinarySerializer.Test.Order
{
    public class BitSizeOrderClass
    {
        [FieldOrder(0)]
        [BitSize(2)]
        public byte First { get; set; }
        
        [FieldOrder(1)]
        [BitSize(6)]
        public byte Second { get; set; }
        
        [FieldOrder(2)]
        public byte Third { get; set; }

        [FieldOrder(3)]
        [BitSize(1)]
        public bool Fourth { get; set; }
                
        [FieldOrder(4)]
        [BitSize(4)]
        public bool Fifth { get; set; }

        [FieldOrder(5)]
        [BitSize(1)]
        public bool Sixth { get; set; }
    }
}