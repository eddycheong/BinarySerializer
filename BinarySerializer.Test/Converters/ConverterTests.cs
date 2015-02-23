using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinarySerializer.Test.Converters
{
    [TestClass]
    public class ConverterTests : TestBase
    {
        [TestMethod]
        public void ConverterTest()
        {
            var expected = new ConverterClass {Field = "FieldValue"};
            var actual = Roundtrip(expected);
            Assert.AreEqual(((double)expected.Field.Length)/2, actual.HalfFieldLength);
            Assert.AreEqual(expected.Field, actual.Field);
        }

        [TestMethod]
        public void ConverterOnFieldOrderAttributeTest()
        {
            var value = new ConverterClass2 {Field = 0x04};

            var stream = new MemoryStream();
            new BinarySerialization.BinarySerializer().Serialize(stream, value);
            stream.Position = 0;
            var data = stream.ToArray();
            
            Assert.AreEqual(0x40, data[0]);
        }

        [TestMethod]
        public void ConverterOnFieldOrderAttributeDeserializationTest()
        {
            var actual = new BinarySerialization.BinarySerializer().Deserialize<ConverterClass2>(new byte[] {0x40});
            
            Assert.AreEqual(0x04, actual.Field);
        }
    }
}
