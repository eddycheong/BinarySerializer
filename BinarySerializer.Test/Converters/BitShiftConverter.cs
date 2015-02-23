using System;
using BinarySerialization;

namespace BinarySerializer.Test.Converters
{
    public class BitShiftConverter : IValueConverter
    {
        public object Convert(object value, object converterParameter, BinarySerializationContext ctx)
        {
            var a = System.Convert.ToByte(value);
            return a << (int)converterParameter;
        }

        public object ConvertBack(object value, object converterParameter, BinarySerializationContext ctx)
        {
            var a = System.Convert.ToByte(value);
            return (byte) (a >> (int)converterParameter);
        }
    }
}