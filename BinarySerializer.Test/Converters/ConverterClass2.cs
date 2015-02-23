using BinarySerialization;

namespace BinarySerializer.Test.Converters
{
    public class ConverterClass2
    {
        [FieldOrder(0, ConverterType = typeof(BitShiftConverter), ConverterParameter = 4)]
        public byte Field { get; set; }
    }
}